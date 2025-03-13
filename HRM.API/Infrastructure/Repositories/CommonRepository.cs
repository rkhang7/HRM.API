using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRM.API.Infrastructure.Repositories
{
    public class CommonRepository : ICommomRepository
    {
        private readonly ApplicationDbContext _context;
        public CommonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CommomEntity> Add(CommomEntity commom)
        {
            var entity = (await  _context.Commoms.AddAsync(commom)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CommomEntity?> getByCode(string code)
        {
            var commom = await _context.Commoms.FindAsync(code);
            return commom;
        }

        public async Task<List<CommomEntity>> getByGroupCode(string groupCode)
        {
            return await  _context.Commoms.Where(c => c.GroupCode == groupCode).ToListAsync();
        }

        public async Task<CommomEntity?> Update(CommomEntity commom)
        {
            var entity =  _context.Commoms.Update(commom).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
