using HRM.API.Domain.Entities;

namespace HRM.API.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> Create(UserEntity user);
        Task<UserEntity?> GetByUserName(string userName);

        Task UpdateUserAsync(UserEntity user);
    }
}
