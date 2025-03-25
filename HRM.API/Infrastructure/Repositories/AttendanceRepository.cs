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

        public async Task<AttendanceEntity?> FindAsync(AttendanceEntity attendance)
        {
            var entity = await _context.Attendances.FindAsync(attendance);
            return entity;
        }

        public async Task<List<AttendanceEntity>> GetAllAsync()
        {
            return await _context.Attendances.ToListAsync();
        }

        public Task<AttendanceEntity?> GetAttendanceToday(int userId)
        {
            var attendance = _context.Attendances
                .FromSqlRaw("EXEC GetAttendanceToday @p0", userId)
                .AsEnumerable()
                .FirstOrDefault();


            return Task.FromResult(attendance);
        }

        public async Task<AttendanceEntity?> UpdateAsync(AttendanceEntity attendance)
        {
            var entity = _context.Attendances.Update(attendance).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
