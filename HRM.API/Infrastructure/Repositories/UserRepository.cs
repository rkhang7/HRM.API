using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            var user = await _context.Users.Include(user => user.Role).Include(user => user.Position).FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }

        public async Task<UserEntity?> GetByEmailToken(string token)
        {
            var user = await _context.Users.Include(user => user.Role).Include(user => user.Position).FirstOrDefaultAsync(user => user.EmailVerificationToken == token);
            return user;
        }

        public async Task<UserEntity?> GetByUserName(string userName)
        {
            var user = await _context.Users.Include(user => user.Role).Include(user => user.Position).FirstOrDefaultAsync(user => user.UserName == userName);
            return user;
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
