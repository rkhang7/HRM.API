using AutoMapper;
using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Role;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Infrastructure.Repositories;

namespace HRM.API.Application.Handlers.Role
{
    public class UpdateRoleHandler : MasterCommandHandler<UpdateRoleDTO>
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<UpdateRoleDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var commom = _mapper.Map<RoleEntity>(request.Data);
                var result = await _roleRepository.Update(commom);
                if (result != null)
                {
                    return ApiResponse<dynamic>.Success(result);
                }
                else
                {
                    return ApiResponse<dynamic>.Error("Can not find");
                }

            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
