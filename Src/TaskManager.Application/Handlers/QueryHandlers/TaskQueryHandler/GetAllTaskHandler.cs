using System;
using MediatR;
using static TaskManager.Application.Queries.TaskQueries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Handlers.QueryHandlers.TaskQueryHandler
{
    public class GetAllTaskHandler : IRequestHandler<GetAllTaskQuery, List<Tasks>>
    {
        private readonly ITaskQueryRepository _taskQueryRepository;
        private readonly ILogger _logger;

        public GetAllTaskHandler(ITaskQueryRepository taskQueryRepository,ILogger logger)
        {
            _taskQueryRepository = taskQueryRepository;
            _logger = logger;
        }
        public async Task<List<Tasks>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            return (List<Tasks>)await _taskQueryRepository.GetAllAsync();
        }
    }
}

