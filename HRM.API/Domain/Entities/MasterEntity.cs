using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.API.Domain.Entities
{
    public class MasterEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Khóa chính

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } 
    }
}
