using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Infrastructure.Data;
using static Azure.Core.HttpHeader;

namespace HRM.API.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RoleEntity> Create(RoleEntity role)
        {
            var entity = (await _context.Role.AddAsync(role)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RoleEntity?> Update(RoleEntity role)
        {
            var entity = _context.Role.Update(role).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
