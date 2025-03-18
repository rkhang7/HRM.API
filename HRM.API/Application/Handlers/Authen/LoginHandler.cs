using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Authen;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Utils.Constants;

namespace HRM.API.Application.Handlers.Authen
{
    public class LoginHandler : MasterQueryHandler<LoginDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;

        public LoginHandler(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ApiResponse<object>> Handle(MasterQuery<LoginDTO> request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _userRepository.GetByUserName(request.Data.UserName);
                if(user != null)
                {

                   
                    if (_authService.VerifyPassword(request.Data.Password, user.Password))
                    {
                        user.Token = _authService.GenerateJwtToken(user);
                        return ApiResponse<dynamic>.Success(user);
                    }
                    else
                    {
                        return ApiResponse<dynamic>.Error(MessageErrorConstants.WrongPassword);
                    }
                }
                else
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.WrongUserName);
                }
                


            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
