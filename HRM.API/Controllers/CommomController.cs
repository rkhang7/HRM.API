using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs.Commom;
using HRM.API.Domain.DTOs.CreateCommomDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    
    public class CommomController: MasterController
    {
        
        public CommomController(IMediator mediator): base(mediator)
        {
           

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] MasterCommand<CreateCommomDto> query)
        {
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] MasterCommand<UpdateCommomDto> query)
        {
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }


    }
}
