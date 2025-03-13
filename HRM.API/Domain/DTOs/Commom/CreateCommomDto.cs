using System.ComponentModel.DataAnnotations;

namespace HRM.API.Domain.DTOs.CreateCommomDto
{
    public class CreateCommomDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string GroupCode { get; set; } = string.Empty;
    }
}
