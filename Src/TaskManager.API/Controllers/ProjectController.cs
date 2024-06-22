using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Command.ProjectCommand;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;
using static TaskManager.Application.Queries.ProjectQueries;

namespace TaskManager.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController:ControllerBase
	{
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
		{
			_mediator = mediator;
		}
        [HttpGet]
        public async Task<List<Project>> Get()
		{
			return await _mediator.Send(new GetAllProjectQuery());
		}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Project> Get(Int64 id)
        {
            return await _mediator.Send(new GetProjectByIdQuery(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectResponse>> CreateProject([FromBody] CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("EditProject/{id}")]
        public async Task<ActionResult> EditProject(int id, [FromBody] EditProjectCommand command)
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

        [HttpDelete("DeleteProject/{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteProjectCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

    }
}	


