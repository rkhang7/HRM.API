using HRM.API.Domain.DTOs;
using HRM.API.Enum;
using MediatR;

namespace HRM.API.Application.Queries
{
    public class MasterQuery<T> : IRequest<ApiResponse<object>>
    {
        public T Data { get; set; }
        public ActionEnum Action { get; set; }

        public String LangCode { get; set; } = String.Empty;
        public int FunctionCode { get; set; }
    }
}
