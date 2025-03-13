using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRM.API.Domain.Entities
{
    public class UserEntity : MasterEntity
    {

        [Column(TypeName = "varchar(50)"), Required]
        public string UserName { get; set; } = string.Empty;
        [Column(TypeName = "varchar(50)"), Required]
        public string FirstName { get; set; } = string.Empty;
        [Column(TypeName = "varchar(50)"), Required]
        public string LastName { get; set; } = string.Empty;
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; } = string.Empty;
        [Column(TypeName = "varchar(50)")]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "varchar(100)"), Required]
        public string Password { get; set; } = string.Empty;
        public bool? EmailVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [ForeignKey(nameof(Position))]
        public int? PositionCode { get; set; }
        public virtual CommomEntity? Position { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Address { get; set; }
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public virtual RoleEntity? Role { get; set; }
        public string? AvatarUrl { get; set; }
        public bool Gender { get; set; }
        public DateTime? HireDate { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Token { get; set; } = string.Empty;
    }
}
