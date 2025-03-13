using HRM.API.Domain.Entities;

namespace HRM.API.Domain.Interfaces
{
    public interface ICommomRepository
    {
        Task<List<CommomEntity>> getByGroupCode(string groupCode);
        Task<CommomEntity?> getByCode(string code);
        Task<CommomEntity> Add(CommomEntity commom);
        Task<CommomEntity?> Update(CommomEntity commom);
    }
}
