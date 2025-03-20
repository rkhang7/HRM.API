using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Authen;

namespace HRM.API.Application.Handlers.Authen
{
    public class RefreshTokenHandler : MasterCommandHandler<RefreshTokenDTO>
    {
        private readonly AuthService _authService;

        public RefreshTokenHandler(AuthService authService)
        {
            _authService = authService;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<RefreshTokenDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var (newAccessToken, newRefreshToken) = await _authService.RefreshTokenAsync(request.Data.RefreshToken);
                return ApiResponse<dynamic>.Success(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
