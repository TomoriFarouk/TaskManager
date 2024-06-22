using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Application.Mapper;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command;
using TaskManager.Core.Interface.Query;

namespace TaskManager.Application.Handlers.CommandHandlers.TaskHandler
{
    public class EditTaskHandler : IRequestHandler<EditTaskCommand, TaskResponse>
    {
        private readonly ILogger _logger;
        private readonly ITaskCommandRepository _taskCommandRepository;
        private readonly ITaskQueryRepository _taskQueryRepository;
        public EditTaskHandler(ITaskCommandRepository taskRepository,
            ITaskQueryRepository taskQueryRepository,ILogger logger
            )
        {
            _logger = logger;
            _taskCommandRepository = taskRepository;
           _taskQueryRepository = taskQueryRepository;
        }
        public async Task<TaskResponse> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            var taskEntity = TaskManagerMapper.Mapper.Map<Tasks>(request);

            if (taskEntity is null)
            {
                _logger.LogError($"There is a problem in mapper");
                throw new ApplicationException("There is a problem in mapper");
            }

            try
            {
                await _taskCommandRepository.UpdateAsync(taskEntity);
            }
            catch (DbUpdateException dbEx)
            {
                var innerExceptionMessage = dbEx.InnerException?.Message;
                var fullErrorMessage = $"An error occurred while updating the task. {dbEx.Message} {innerExceptionMessage}";
                _logger.LogError(fullErrorMessage);
                throw new ApplicationException(fullErrorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                throw new ApplicationException($"An unexpected error occurred: {ex.Message}");
            }


            var tasks = await _taskQueryRepository.GetAllAsync();
            var modifiedTask = tasks.FirstOrDefault(x => x.Id == request.Id);
            if (modifiedTask == null)
            {
                _logger.LogError($"Task with ID {request.Id}was not found after update.");
                throw new ApplicationException($"Task with ID {request.Id} was not found after update.");
            }
            var taskResponse = TaskManagerMapper.Mapper.Map<TaskResponse>(tasks);
            _logger.LogError($"{taskResponse}");
            return taskResponse;
        }
    }
}

