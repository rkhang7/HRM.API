using HRM.API.Application.Commands;
using HRM.API.Application.Commands.Authen;
using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs.Authen;
using HRM.API.Domain.DTOs.CreateCommomDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HRM.API.Controllers
{
    public class AuthenController : MasterController
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

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var command = new VerifyEmailCommand();
            command.Token = token;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] MasterCommand<ForgotPasswordDTO> command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] MasterCommand<RefreshTokenDTO> command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
