using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.API.Domain.Entities
{
    public class LogEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Column(TypeName = "varchar(10)"), Required]
        public string Method { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)"), Required]
        public string Path { get; set; } = string.Empty;

        [Column(TypeName = "varchar(500)")]
        public string QueryString { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string RequestBody { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ResponseBody { get; set; } = string.Empty;
    }
}