
using AutoMapper;
using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Users;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Infrastructure.Repositories;
using HRM.API.Utils;
using Microsoft.EntityFrameworkCore;

namespace HRM.API.Application.Handlers.User
{
    public class CreateUserHandler : MasterCommandHandler<CreateUserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<CreateUserDTO> request, CancellationToken cancellationToken)
        {
            try
            {

                var user = _mapper.Map<UserEntity>(request.Data);
                var result = await _userRepository.Create(user);

                var response = await _userRepository.GetByUserName(result.UserName);
                if(response == null)
                {
                    return ApiResponse<dynamic>.Error(Constants.messageError);
                }
                else
                {
                    return ApiResponse<dynamic>.Success(response);
                }

                   
            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
