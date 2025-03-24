﻿using HRM.API.Domain.Entities;

namespace HRM.API.Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<AttendanceEntity> CreateAsync(AttendanceEntity attendance);
        Task<List<AttendanceEntity>> GetAllAsync();
    }
}
