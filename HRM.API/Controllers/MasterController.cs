using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController(IMediator mediator) : ControllerBase
    {
        protected readonly IMediator _mediator = mediator;
    }
}
