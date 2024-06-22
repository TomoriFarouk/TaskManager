using System;
using MediatR;
using TaskManager.Application.Command.ProjectCommand;
using TaskManager.Application.Mapper;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command;
using TaskManager.Core.Interface.Query;
using TaskManager.Core.Entities;
using Microsoft.Extensions.Logging;

namespace ProjectManager.Application.Handlers.CommandHandlers.ProjectHandler
{
    public class EditProjectHandler : IRequestHandler<EditProjectCommand, ProjectResponse>
    {
        private readonly ILogger _logger;
        private readonly IProjectCommandRepository _ProjectCommandRepository;
        private readonly IProjectQueryRepository _ProjectQueryRepository;
        public EditProjectHandler(IProjectCommandRepository ProjectRepository,
            IProjectQueryRepository ProjectQueryRepository,ILogger logger
            )
        {
            _logger = logger;
            _ProjectCommandRepository = ProjectRepository;
            _ProjectQueryRepository = ProjectQueryRepository;
        }
        public async Task<ProjectResponse> Handle(EditProjectCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            var ProjectEntity = TaskManagerMapper.Mapper.Map<Project>(request);

            if (ProjectEntity is null)
            {
                _logger.LogError($"There is a problem in mapper");
                throw new ApplicationException("There is a problem in mapper");
            }

            try
            {
                await _ProjectCommandRepository.UpdateAsync(ProjectEntity);
            }
            catch (Exception exp)
            {
                _logger.LogError($"{exp.Message}");
                throw new ApplicationException(exp.Message);
            }

            var Projects = await _ProjectQueryRepository.GetAllAsync();
            var modifiedProject = Projects.FirstOrDefault(x => x.Id == request.Id);
            var ProjectResponse = TaskManagerMapper.Mapper.Map<ProjectResponse>(modifiedProject);
            _logger.LogError($"{ProjectResponse}");
            return ProjectResponse;
        }
    }
}

