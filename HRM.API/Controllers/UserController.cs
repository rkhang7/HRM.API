using HRM.API.Application.Commands;
using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs.CreateCommomDto;
using HRM.API.Domain.DTOs.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    public class UserController: MasterController
    {
        public UserController(IMediator mediator) : base(mediator)
        {

        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] MasterCommand<CreateUserDTO> query)
        {
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }

    
        [HttpPost("[action]")]
        public async Task<IActionResult> GetByUserName([FromBody] MasterQuery<GetByUserNameUserDTO> query)
        {
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetAll([FromBody] MasterQuery<GetAllUserDTO> query)
        {
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] MasterCommand<UpdateUserDTO> query)
        {
            query.UserName = Username;
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }
    }
}
