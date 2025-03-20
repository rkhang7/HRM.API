using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Authen;
using HRM.API.Domain.Interfaces;
using HRM.API.Utils.Constants;

namespace HRM.API.Application.Handlers.Authen
{
    public class ForgotPasswordHandler : MasterCommandHandler<ForgotPasswordDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;
        private readonly IEmailService _emailServicel;

        public ForgotPasswordHandler(IUserRepository userRepository, AuthService authService, IEmailService emailServicel)
        {
            _userRepository = userRepository;
            _authService = authService;
            _emailServicel = emailServicel;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<ForgotPasswordDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByEmail(request.Data.Email ?? "");

                if (user == null)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.NotFound);
                }

                string newPassword = GenerateRandomPassword();
                string hashedPassword = _authService.HashPassword(newPassword);
                user.Password = hashedPassword;
                await _userRepository.UpdateUserAsync(user);

                await _emailServicel.SendEmailForgotPasswordAsync(request.Data.Email ?? "", newPassword);

                return ApiResponse<dynamic>.Success(MessageSuccessConstants.ForgotEmailSuccess);

            }
            catch (Exception e)
            {
                return ApiResponse<dynamic>.Error(e.Message);
            }
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$%^&*!";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
