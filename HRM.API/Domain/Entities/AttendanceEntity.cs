using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace HRM.API.Domain.Entities
{
    public class AttendanceEntity : MasterEntity
    {
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        [ForeignKey(nameof(User)), Required]
        public int UserId { get; set; }
        public virtual UserEntity? User { get; set; }

        [ForeignKey(nameof(Status))]
        public string? StatusCode { get; set; }
        public virtual CommomEntity? Status { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Desciption { get; set; } = string.Empty;

        [Column(TypeName = "varchar(500)")]
        public string? CheckInDescription { get; set; } = string.Empty;
        [Column(TypeName = "varchar(500)")]
        public string? CheckOutDescription { get; set; } = string.Empty;

        [Column(TypeName = "varchar(500)"), Required]
        public string? CheckInFaceUrl { get; set; } = string.Empty;

        [Column(TypeName = "varchar(500)")]
        public string? CheckoutFaceUrl { get; set; } = string.Empty;
        [Column(TypeName = "varchar(20)"), Required]
        public string? Ip { get; set; } = string.Empty;
        [Required]
        public decimal? Lat { get; set; }
        [Required]
        public decimal? Long { get; set; }
    }
}
