using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Command.NotificationCommand;
using TaskManager.Application.Command.TaskCommand;

namespace TaskManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("ReadStatus")]
        public async Task<ActionResult> ReadStatus(int id)
        {
            var notification = await _mediator.Send(new IsReadCommand(id));
            return Ok(notification);
        }
    }
}

