using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.API.Domain.Entities
{
    public class RoleEntity: MasterEntity
    {
        [Required]
        public int Type { get; set; }
        [Column(TypeName = "varchar(50)"), Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<UserEntity>?  Users { get; set; }
    }
}
