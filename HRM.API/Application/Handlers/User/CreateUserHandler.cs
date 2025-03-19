
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
        private readonly IEmailService _emailService;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IConfiguration config, AuthService authService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
            _authService = authService;
            _emailService = emailService;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<CreateUserDTO> request, CancellationToken cancellationToken)
        {
            try
            {



                var user = _mapper.Map<UserEntity>(request.Data);


                user.Password = _authService.HashPassword(user.Password);

                // Tạo token xác thực
                var token = Guid.NewGuid().ToString();
                user.EmailVerificationToken = token;
                user.EmailVerificationTokenExpires = DateTime.UtcNow.AddHours(24);
                user.EmailVerified = false;

                var result = await _userRepository.Create(user);

               

                var response = await _userRepository.GetByUserName(result.UserName);

                // Gửi email xác thực
                await _emailService.SendEmailVerificationAsync(user.Email, token);

                if (response == null)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.Default);
                }
                else
                {
                    response.Password = _authService.HashPassword(response.Password);
                    response.Token = _authService.GenerateJwtToken(response);
                    return ApiResponse<dynamic>.Success(response, message: MessageErrorConstants.VerifyEmail);
                }

                   
            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }

       
    }
}
