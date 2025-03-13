using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using MediatR;
using HRM.API.Application.Queries;

namespace HRM.API.Application.Handlers
{
    public interface MasterQueryHandler<T> : IRequestHandler<MasterQuery<T>, ApiResponse<object>>
    {
        
    }
}
