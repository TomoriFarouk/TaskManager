using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Command.ProjectCommand;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;
using static TaskManager.Application.Queries.TaskQueries;

namespace TaskManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpGet]
        public async Task<List<Tasks>> Get()
        {
            return await _mediator.Send(new GetAllTaskQuery());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Tasks> Get(Int64 id)
        {
            return await _mediator.Send(new GetTaskByIdQuery(id));
        }
        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Tasks> GetTaskByStatus(string status)
        {
            return await _mediator.Send(new GetTaskByStatusQuery(status));
        }

        [HttpGet("priority/{priority}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Tasks> GetTaskByPriority(string priority)
        {
            return await _mediator.Send(new GetTaskByPriorityQuery(priority));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TaskResponse>> CreateTask([FromBody] CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("EditTask/{id}")]
        public async Task<ActionResult> EditTask(int id, [FromBody] EditTaskCommand command)
        {
            try
            {
                if (command.Id == id)
                {
                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }


        }

        [HttpDelete("DeleteTask/{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteTaskCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("due-this-week")]
        public async Task<ActionResult> GetTasksDueForCurrentWeek()
        {
            var tasks = await _mediator.Send(new GetAllTaskDueForTheWeekQuery());
            return Ok(tasks);
        }

        [HttpGet("ToggleIsCompleted")]
        public async Task<ActionResult> ToggleIsCompleted(int id)
        {
            var tasks = await _mediator.Send(new ToggleIsCompletedCommand(id));
            return Ok(tasks);
        }

    }
}

