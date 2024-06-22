using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.ProjectCommand;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Application.Mapper;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command;

namespace TaskManager.Application.Handlers.CommandHandlers.ProjectHandler
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectResponse>
    {
        private readonly IProjectCommandRepository _projectCommandRepository;
        private readonly ILogger _logger;

        public CreateProjectHandler(IProjectCommandRepository projectCommandRepository,ILogger logger)
        {
            _projectCommandRepository = projectCommandRepository;
            _logger = logger;
        }
        public async Task<ProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            var taskEntity = TaskManagerMapper.Mapper.Map<Project>(request);

            if (taskEntity is null)
            {
                _logger.LogError($"There is a problem in mapper");
                throw new ApplicationException("There is a problem in mapper");
            }

            var newProject = await _projectCommandRepository.AddAsync(taskEntity);
            var projectResponse = TaskManagerMapper.Mapper.Map<ProjectResponse>(newProject);
            _logger.LogError($"{projectResponse}");
            return projectResponse;
        }
    }
}

