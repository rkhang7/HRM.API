using AutoMapper;
using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Users;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Utils.Constants;

namespace HRM.API.Application.Handlers.User
{
    public class UpdateUserHandler : MasterCommandHandler<UpdateUserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(MasterCommand<UpdateUserDTO> request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _userRepository.GetByUserName(request.UserName ?? "");

                if (user == null)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.NotFound);
                }

                if (request.Data.FirstName != null)
                    user.FirstName = request.Data.FirstName;
                if (request.Data.LastName != null)

                    user.LastName = request.Data.LastName;
                if (request.Data.Address != null)
                    user.Address = request.Data.Address;

                if (request.Data.AvatarUrl != null)
                    user.AvatarUrl = request.Data.AvatarUrl;
   
                 user.Gender = request.Data.Gender;
                if (request.Data.HireDate != null)
                    user.HireDate = request.Data.HireDate;

                await _userRepository.UpdateUserAsync(user);

                return ApiResponse<dynamic>.Success(MessageSuccessConstants.Default);
            }
            catch (Exception e)
            {
                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
