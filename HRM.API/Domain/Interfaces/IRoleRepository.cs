using HRM.API.Domain.Entities;

namespace HRM.API.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<RoleEntity> Create(RoleEntity role);
        Task<RoleEntity?> Update(RoleEntity role);
    }
}
