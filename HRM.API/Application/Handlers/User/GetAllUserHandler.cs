using AutoMapper;
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
        private readonly IMapper _mapper;
        public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> Handle(MasterQuery<GetAllUserDTO> request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var usersDTO = _mapper.Map<List<UserResponseDTO>>(users);
            return ApiResponse<dynamic>.Success(usersDTO);
        }
    }
}
