using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.API.Domain.Entities
{
    public class CommomEntity
    {
       
        [Required]
        [Key]
        [Column(TypeName = "varchar(12)")]
        public string Code { get; set; } = string.Empty;
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string GroupCode { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public ICollection<UserEntity>? Users { get; set; }
    }
}
