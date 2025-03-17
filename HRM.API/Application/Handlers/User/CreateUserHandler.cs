
using AutoMapper;
using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Users;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Cryptography;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HRM.API.Utils.Constants;

namespace HRM.API.Application.Handlers.User
{
    public class CreateUserHandler : MasterCommandHandler<CreateUserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly AuthService _authService;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IConfiguration config, AuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
            _authService = authService;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<CreateUserDTO> request, CancellationToken cancellationToken)
        {
            try
            {

                var user = _mapper.Map<UserEntity>(request.Data);
                user.Password = _authService.HashPassword(user.Password);
                var result = await _userRepository.Create(user);

                var response = await _userRepository.GetByUserName(result.UserName);
              
                if (response == null)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.Default);
                }
                else
                {
                    response.Password = _authService.HashPassword(response.Password);
                    response.Token = _authService.GenerateJwtToken(response);
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
