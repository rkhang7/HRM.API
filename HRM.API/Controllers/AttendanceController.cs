using HRM.API.Application.Commands;
using HRM.API.Application.Queries;
using HRM.API.Domain.DTOs.Attendance;
using HRM.API.Domain.DTOs.Authen;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    public class AttendanceController: MasterController
    {
        public AttendanceController(IMediator mediator) : base(mediator)
        {


        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromForm] CreateAttendanceDTO command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
