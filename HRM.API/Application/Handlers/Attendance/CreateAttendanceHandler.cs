using AutoMapper;
using Azure.Core;
using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Attendance;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using MediatR;

namespace HRM.API.Application.Handlers.Attendance
{
    public class CreateAttendanceHandler : IRequestHandler<CreateAttendanceDTO, ApiResponse<object>>
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateAttendanceHandler(IMapper mapper, IAttendanceRepository attendanceRepository, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _attendanceRepository = attendanceRepository;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<object>> Handle(CreateAttendanceDTO request, CancellationToken cancellationToken)
        {
            
            var attendance = _mapper.Map<AttendanceEntity>(request);


            var attendanceToday = await _attendanceRepository.GetAttendanceToday(request.UserId);

            if (attendanceToday != null)
            {
                // checkout
                attendanceToday.CheckOutTime = DateTime.Now;


                string? checkOutImageUrl = request.CheckOutFaceImage != null ? await SaveImageAsync(request.CheckOutFaceImage) : null;

                if (checkOutImageUrl != null)
                {
                    attendanceToday.CheckoutFaceUrl = checkOutImageUrl;
                }

                await _attendanceRepository.UpdateAsync(attendanceToday);

                return ApiResponse<dynamic>.Success(attendanceToday);
            }

            else
            {
                attendance.CheckInTime = DateTime.Now;

                string? checkInImageUrl = request.CheckInFaceImage != null ? await SaveImageAsync(request.CheckInFaceImage) : null;

                if (checkInImageUrl != null)
                {
                    attendance.CheckInFaceUrl = checkInImageUrl;
                }

                await _attendanceRepository.CreateAsync(attendance);

                return ApiResponse<dynamic>.Success(attendance);
            }

     


           
        }

        // Hàm lưu ảnh vào thư mục và trả về URL
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "faces");

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Đặt tên file với GUID để tránh trùng lặp
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadPath, fileName);

            // Lấy thông tin request hiện tại
            var request = _httpContextAccessor.HttpContext.Request;
            string imageUrl = $"{request.Scheme}://{request.Host}/faces/{fileName}";

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về URL ảnh để hiển thị trên frontend
            return imageUrl;
        }

    }
}
