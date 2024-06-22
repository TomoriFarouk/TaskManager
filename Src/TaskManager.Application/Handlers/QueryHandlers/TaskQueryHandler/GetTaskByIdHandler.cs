using System;
using MediatR;
using static TaskManager.Application.Queries.TaskQueries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Handlers.QueryHandlers.TaskQueryHandler
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, Tasks>
    {
        private readonly IMediator _mediator;
        private readonly ITaskQueryRepository _taskQueryRepository;
        private readonly ILogger _logger;

        public GetTaskByIdHandler(IMediator mediator, ITaskQueryRepository taskQueryRepository,ILogger logger)
        {
            _mediator = mediator;
            _taskQueryRepository = taskQueryRepository;
            _logger = logger;
        }
        public async Task<Tasks> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            var task = await _mediator.Send(new GetAllTaskQuery());
            var selectedTask = task.FirstOrDefault(x => x.Id == request.Id);
            _logger.LogError($"{selectedTask}");
            return selectedTask;

            //var customers = await _customerQueryRepository.GetByIdAsync(request.Id);
            //return customers;
        }
    }
}
