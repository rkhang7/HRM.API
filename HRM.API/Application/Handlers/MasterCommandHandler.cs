using HRM.API.Application.Commands;
using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs;
using MediatR;

namespace HRM.API.Application.Handlers
{
    public interface MasterCommandHandler<T>: IRequestHandler<MasterCommand<T>, ApiResponse<object>>
    {
    }
}
