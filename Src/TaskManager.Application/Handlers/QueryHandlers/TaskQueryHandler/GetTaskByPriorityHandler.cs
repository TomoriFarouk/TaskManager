using System;
using MediatR;
using static TaskManager.Application.Queries.TaskQueries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Handlers.QueryHandlers.TaskQueryHandler
{
    public class GetTaskByPriorityHandler : IRequestHandler<GetTaskByPriorityQuery, Tasks>
    {
        private readonly IMediator _mediator;
        private readonly ITaskQueryRepository _taskQueryRepository;
        private readonly ILogger _logger;

        public GetTaskByPriorityHandler(IMediator mediator, ITaskQueryRepository taskQueryRepository,ILogger logger)
        {
            _mediator = mediator;
            _taskQueryRepository = taskQueryRepository;
            _logger = logger;
        }
        public async Task<Tasks> Handle(GetTaskByPriorityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{request}");
                var taskList = await _mediator.Send(new GetAllTaskQuery());

                // Convert enum to string
                string priorityString = request.priority.ToString();

                // Find the task with the matching status
                var selectedTask = taskList.FirstOrDefault(x => x.priority.ToString() == priorityString);
                _logger.LogError($"{selectedTask}");
                return selectedTask;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError($"An error occurred while retrieving tasks by priority, {ex.Message}");
                throw new ApplicationException("An error occurred while retrieving tasks by priority.", ex);
            }
        }
    }


    }


