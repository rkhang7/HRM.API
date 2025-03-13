using AutoMapper;
using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.Commom;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;

namespace HRM.API.Application.Handlers.Commom
{
    public class UpdateCommomHandler : MasterCommandHandler<UpdateCommomDto>
    {
        private readonly ICommomRepository _commomRepository;
        private readonly IMapper _mapper;

        public UpdateCommomHandler(ICommomRepository commomRepository, IMapper mapper)
        {
            _commomRepository = commomRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> Handle(MasterCommand<UpdateCommomDto> request, CancellationToken cancellationToken)
        {
            try
            {
                var commom = _mapper.Map<CommomEntity>(request.Data); 
                var result = await _commomRepository.Update(commom);
                if(result != null)
                {
                    return ApiResponse<dynamic>.Success(result);
                }
                else
                {
                    return ApiResponse<dynamic>.Error("Can not find");
                }
                
            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
