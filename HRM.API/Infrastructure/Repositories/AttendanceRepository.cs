using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace HRM.API.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AttendanceEntity> CreateAsync(AttendanceEntity attendance)
        {
            var entity = (await _context.Attendances.AddAsync(attendance)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<AttendanceEntity>> GetAllAsync()
        {
            return await _context.Attendances.ToListAsync();
        }
    }
}
