using HRM.API.Domain.DTOs;
using MediatR;

namespace HRM.API.Application.Commands.Authen
{
    public class VerifyEmailCommand: IRequest<ApiResponse<object>>
    {
        public String Token { get; set; } = string.Empty;
    }
}
