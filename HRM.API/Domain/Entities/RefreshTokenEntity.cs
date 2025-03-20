using HRM.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RefreshTokenEntity
{
    [Key] // Đánh dấu đây là khóa chính
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng
    public int Id { get; set; }

    [Required] // Bắt buộc, không được null
    [Column(TypeName = "nvarchar(450)")] // Cấu hình kiểu dữ liệu trong database

    public string Token { get; set; } = string.Empty;

    [Required]
    [ForeignKey(nameof(User))] // Đánh dấu đây là khóa ngoại tham chiếu đến User
    public int UserId { get; set; }

    [Required]
    public DateTime ExpiresAt { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; } // Cho phép null

    // Navigation property
    public virtual UserEntity User { get; set; }
}