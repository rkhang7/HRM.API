using System.ComponentModel.DataAnnotations;

namespace HRM.API.Domain.DTOs
{
    public class CommonDto
    {
   
        public String Code { get; set; } = String.Empty;

        public String Name { get; set; } = String.Empty;
        public String GroupCode { get; set; } = String.Empty;
        public bool Active { get; set; } = true;
    }
}
