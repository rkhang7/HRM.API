using AutoMapper;
using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Role;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;

namespace HRM.API.Application.Handlers.Role
{
    public class CreateRoleHandler : MasterCommandHandler<CreateRoleDTO>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<CreateRoleDTO> request, CancellationToken cancellationToken)
        {
            try
            {

                var role = _mapper.Map<RoleEntity>(request.Data);
                var result = await _roleRepository.Create(role);
                return ApiResponse<dynamic>.Success(result);
            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
