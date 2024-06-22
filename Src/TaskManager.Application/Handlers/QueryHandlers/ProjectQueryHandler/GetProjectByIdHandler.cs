using System;
using MediatR;
using static TaskManager.Application.Queries.TaskQueries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using static TaskManager.Application.Queries.ProjectQueries;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Handlers.QueryHandlers.ProjectQueryHandler
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, Project>
    {
        private readonly IMediator _mediator;
        private readonly IProjectQueryRepository _projectQueryRepository;
        private readonly ILogger _logger;

        public GetProjectByIdHandler(IMediator mediator, IProjectQueryRepository projectQueryRepository,ILogger logger)
        {
            _mediator = mediator;
            _projectQueryRepository = projectQueryRepository;
            _logger = logger;
        }
        public async Task<Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            var project = await _mediator.Send(new GetAllProjectQuery());
            var selectedProject = project.FirstOrDefault(x => x.Id == request.Id);
            _logger.LogError($"{selectedProject}");
            return selectedProject;

            //var customers = await _customerQueryRepository.GetByIdAsync(request.Id);
            //return customers;
        }
    }
}

