using System;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.ProjectCommand;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Core.Interface.Command;
using TaskManager.Core.Interface.Query;

namespace TaskManager.Application.Handlers.CommandHandlers.ProjectHandler
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, String>
    {
        private readonly ILogger _logger;
        private readonly IProjectCommandRepository _projectCommandRepository;

        private readonly IProjectQueryRepository _projectQueryRepository;
        public DeleteProjectHandler(IProjectCommandRepository projectRepository
            , IProjectQueryRepository projectQueryRepository,ILogger logger
            )
        {
            _logger = logger;
            _projectCommandRepository = projectRepository;
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task<string> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{request}");

                var projectEntity = await _projectQueryRepository.GetByIdAsync(request.Id);

                await _projectCommandRepository.DeleteAsync(projectEntity);
            }
            catch (Exception exp)
            {
                _logger.LogError($"{exp.Message}");
                throw (new ApplicationException(exp.Message));
            }
            _logger.LogError($"Customer information has been deleted!");
            return "Customer information has been deleted!";
        }
    }
}

