using HRM.API.Application.Commands;
using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs.Authen;
using HRM.API.Domain.DTOs.CreateCommomDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var commom = await _mediator.Send(query);
            return Ok(commom);
        }
    }
}
