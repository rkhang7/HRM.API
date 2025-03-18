namespace HRM.API.Domain.DTOs.Users
{
    public class UpdateUserDTO
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public bool Gender { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
