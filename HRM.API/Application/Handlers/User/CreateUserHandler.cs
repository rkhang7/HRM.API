
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

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<CreateUserDTO> request, CancellationToken cancellationToken)
        {
            try
            {

                var user = _mapper.Map<UserEntity>(request.Data);
                var result = await _userRepository.Create(user);

                var response = await _userRepository.GetByUserName(result.UserName);
              
                if (response == null)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.Default);
                }
                else
                {
                    response.Password = HashPassword(response.Password);
                    response.Token = GenerateJwtToken(response);
                    return ApiResponse<dynamic>.Success(response);
                }

                   
            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var jwtSettings = _config.GetSection("Jwt");

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", user.Id.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
