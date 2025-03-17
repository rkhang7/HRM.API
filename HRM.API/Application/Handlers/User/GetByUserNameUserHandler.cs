using AutoMapper;
using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Users;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Utils;
using HRM.API.Utils.Constants;
namespace HRM.API.Application.Handlers.User
{
    public class GetByUserNameUserHandler : MasterQueryHandler<GetByUserNameUserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public GetByUserNameUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> Handle(MasterQuery<GetByUserNameUserDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepository.GetByUserName(request.Data.UserName);

                if (result == null)
                {
                    return ApiResponse<dynamic>.Error(MessageErrorConstants.NotFound);
                }
                else
                {
           
                    return ApiResponse<dynamic>.Success(result);
                }


            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
