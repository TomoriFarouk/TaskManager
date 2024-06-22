using System;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Core.Interface.Command;
using TaskManager.Core.Interface.Logger;
using TaskManager.Core.Interface.Query;

namespace TaskManager.Application.Handlers.CommandHandlers.TaskHandler
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, String>
    {
        private readonly ILogger _logger;
        private readonly ITaskCommandRepository _taskCommandRepository;

        private readonly ITaskQueryRepository _taskQueryRepository;
        public DeleteTaskHandler(ILogger logger, ITaskCommandRepository taskRepository
            , ITaskQueryRepository taskQueryRepository
            )
        {
            _logger = logger;
            _taskCommandRepository = taskRepository;
           _taskQueryRepository = taskQueryRepository;
        }

        public async Task<string> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {

            try
            {
                _logger.LogInformation($"{request}");
                var customerEntity = await _taskQueryRepository.GetByIdAsync(request.Id);

                await _taskCommandRepository.DeleteAsync(customerEntity);
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

