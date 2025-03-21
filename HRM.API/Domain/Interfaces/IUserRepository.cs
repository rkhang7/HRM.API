﻿using HRM.API.Domain.Entities;

namespace HRM.API.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> Create(UserEntity user);
        Task<UserEntity?> GetByUserName(string userName);
        Task UpdateUserAsync(UserEntity user);
        Task<List<UserEntity>> GetAllAsync();
        Task<UserEntity?> GetByEmailToken(string token);

        Task<UserEntity?> GetByEmail(string email);
    }
}
