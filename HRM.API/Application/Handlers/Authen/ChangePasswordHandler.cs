using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Authen;
using HRM.API.Domain.Interfaces;
using HRM.API.Utils.Constants;
using Microsoft.EntityFrameworkCore;


namespace HRM.API.Application.Handlers.Authen
{
    public class ChangePasswordHandler : MasterCommandHandler<ChangePasswordDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;

        public ChangePasswordHandler(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;

        }

        public async Task<ApiResponse<object>> Handle(MasterCommand<ChangePasswordDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByUserName(request.UserName ?? "");

                if (user == null)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.NotFound);
                }

                // Kiểm tra mật khẩu cũ
                if (!_authService.VerifyPassword(request.Data.CurrentPassword, user.Password))
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.WrongPassword);
                }
                // Hash mật khẩu mới và lưu vào DB
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Data.NewPassword);
                await _userRepository.UpdateUserAsync(user);
                return ApiResponse<dynamic>.Error("Change password success");

            } catch (Exception e)
            {
                return ApiResponse<dynamic>.Error(e.Message);
            }
           

        }
    }
}
