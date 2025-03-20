using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRM.API.Domain.DTOs.Commom
{
    public class CommomResponseDTO
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string GroupCode { get; set; } = string.Empty;
    }
}
