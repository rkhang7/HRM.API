using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs.CreateCommomDto;
using HRM.API.Domain.DTOs.Role;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    public class RoleController: MasterController
    {
        public RoleController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] MasterCommand<CreateRoleDTO> query)
        {
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] MasterCommand<UpdateRoleDTO> query)
        {
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }
    }
}
