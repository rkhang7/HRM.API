using HRM.API.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRM.API.Domain.DTOs.Users
{
    public class CreateUserDTO
    {
        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string Password { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string? PositionCode { get; set; }

        public string? Address { get; set; }

        public int? RoleId { get; set; } = 1;
        public bool Gender { get; set; }
        public DateTime? HireDate { get; set; }


    }
}
