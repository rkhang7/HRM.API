using HRM.API.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace HRM.API.Domain.DTOs.Attendance
{
    public class CreateAttendanceDTO : IRequest<ApiResponse<object>>
    {

        public int UserId { get; set; }
        public string StatusCode { get; set; } = string.Empty;
        public string? Desciption { get; set; } = string.Empty;
        public string? CheckInDescription { get; set; } = string.Empty;
        public string? CheckOutDescription { get; set; } = string.Empty;
        public IFormFile? CheckInFaceImage { get; set; }
        public IFormFile? CheckOutFaceImage { get; set; }
        public string? Ip { get; set; } = string.Empty;
        [Required]
        public decimal? Lat { get; set; }
        [Required]
        public decimal? Long { get; set; }
    }
}
