using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Users;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;

namespace HRM.API.Application.Handlers.User
{
    public class GetAllUserHandler : MasterQueryHandler<GetAllUserDTO>
    {

        private readonly IUserRepository _userRepository;
        public GetAllUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResponse<object>> Handle(MasterQuery<GetAllUserDTO> request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return ApiResponse<dynamic>.Success(users);
        }
    }
}
