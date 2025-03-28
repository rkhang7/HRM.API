﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRM.API.Domain.DTOs.Role
{
    public class RoleResponseDTO
    {
        public int Type { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
