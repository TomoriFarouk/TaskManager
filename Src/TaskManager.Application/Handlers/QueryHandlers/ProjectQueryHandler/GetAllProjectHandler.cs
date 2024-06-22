using System;
using MediatR;
using static TaskManager.Application.Queries.TaskQueries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using static TaskManager.Application.Queries.ProjectQueries;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Handlers.QueryHandlers.ProjectQueryHandler
{
    public class GetAllProjectHandler : IRequestHandler<GetAllProjectQuery, List<Project>>
    {
        private readonly ILogger _logger;
        private readonly IProjectQueryRepository _projectQueryRepository;

        public GetAllProjectHandler(IProjectQueryRepository projectQueryRepository,ILogger logger)
        {
            _logger = logger;
            _projectQueryRepository = projectQueryRepository;
        }
        public async Task<List<Project>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            return (List<Project>)await _projectQueryRepository.GetAllAsync();
        }
    }
}

