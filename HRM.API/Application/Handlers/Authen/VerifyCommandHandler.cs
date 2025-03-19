using HRM.API.Application.Commands.Authen;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.Interfaces;
using HRM.API.Utils.Constants;
using MediatR;

namespace HRM.API.Application.Handlers.Authen
{
    public class VerifyCommandHandler : IRequestHandler<VerifyEmailCommand, ApiResponse<object>>
    {
        private readonly IUserRepository _userRepository;
        public VerifyCommandHandler(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResponse<object>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByEmailToken(request.Token);
                
                if(user == null)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.NotFound);
                }

                if (user.EmailVerificationTokenExpires < DateTime.UtcNow)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.TokenExpired);
                }

                user.EmailVerified = true;
                user.EmailVerificationToken = null;
                user.EmailVerificationTokenExpires = null;
                await _userRepository.UpdateUserAsync(user);

                return ApiResponse<dynamic>.Success(null);


            } catch (Exception e)
            {
                return ApiResponse<dynamic>.Error(e.Message);
            }
           
        }
    }
}
