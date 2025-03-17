using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRM.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserEntity> Create(UserEntity user)
        {
            var entity = (await _context.Users.AddAsync(user)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserEntity?> GetByUserName(string userName)
        {
            var user = await _context.Users.Include(user => user.Role).Include(user => user.Position).FirstOrDefaultAsync(user => user.UserName == userName);
            return user;
        }
    }
}
