using HRM.API.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HRM.API.Domain.DTOs.Commom;
using HRM.API.Domain.DTOs.Role;

namespace HRM.API.Domain.DTOs.Users
{
    public class UserResponseDTO
    {

        public string UserName { get; set; } = string.Empty;
  
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual CommomResponseDTO? Position { get; set; }

        public string? Address { get; set; }

        public virtual RoleResponseDTO? Role { get; set; }
        public string? AvatarUrl { get; set; }
        public bool Gender { get; set; }
        public DateTime? HireDate { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
