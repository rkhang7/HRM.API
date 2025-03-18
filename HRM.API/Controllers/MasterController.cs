using HRM.API.Utils.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterController(IMediator mediator) : ControllerBase
    {
        public string Username
        {
            get
            {
                try
                {
                    return User.FindFirst(p => p.Type == ClaimTypes.Name)!.Value;
                }
                catch (Exception)
                {
                    return "NON";
                }
            }
        }
        protected readonly IMediator _mediator = mediator;
    }
}
