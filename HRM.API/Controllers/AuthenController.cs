using HRM.API.Application.Commands;
using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs.Authen;
using HRM.API.Domain.DTOs.CreateCommomDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM.API.Controllers
{
    public class AuthenController: MasterController
    {
        public AuthenController(IMediator mediator) : base(mediator)
        {


        }
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] MasterQuery<LoginDTO> query)
        {
            var result = await _mediator.Send(query);
          
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword([FromBody] MasterCommand<ChangePasswordDTO> command)
        {
            var _userName = Username;
            command.UserName = _userName;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
