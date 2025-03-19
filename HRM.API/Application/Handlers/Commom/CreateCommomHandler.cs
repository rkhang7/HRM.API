using AutoMapper;
using HRM.API.Application.Commands;
using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs;
using HRM.API.Domain.DTOs.CreateCommomDto;
using HRM.API.Domain.Entities;
using HRM.API.Domain.Interfaces;
using HRM.API.Utils.Constants;
using MediatR;

namespace HRM.API.Application.Handlers.Commom
{
    public class CreateCommomHandler : MasterCommandHandler<CreateCommomDto>
    {
        private readonly ICommomRepository _commomRepository;
        private readonly IMapper _mapper;

        public CreateCommomHandler(ICommomRepository commomRepository, IMapper mapper)
        {
            _commomRepository = commomRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(MasterCommand<CreateCommomDto> request, CancellationToken cancellationToken)
        {
            try
            {
               
                var commom = _mapper.Map<CommomEntity>(request.Data);
                var result = await _commomRepository.Add(commom);
                return ApiResponse<dynamic>.Success(result);
            }
            catch (Exception e)
            {

                return ApiResponse<dynamic>.Error(e.Message);
            }
        }
    }
}
