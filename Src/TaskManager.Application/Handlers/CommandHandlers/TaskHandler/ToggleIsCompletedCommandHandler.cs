using System;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.NotificationCommand;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Mapper;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command;
using TaskManager.Core.Interface.Query;

namespace TaskManager.Application.Handlers.CommandHandlers.TaskHandler
{
    public class ToggleIsCompletedCommandHandler : IRequestHandler<ToggleIsCompletedCommand, string>
    {
        private readonly ILogger _logger;
        private readonly ITaskCommandRepository _taskCommandRepository;
        private readonly ITaskQueryRepository _taskQueryRepository;
        private readonly IMediator _mediator;

        public ToggleIsCompletedCommandHandler(ITaskCommandRepository taskRepository,
            ITaskQueryRepository taskQueryRepository, IMediator mediator,ILogger logger
            )
        {
            _logger = logger;
            _taskCommandRepository = taskRepository;
            _taskQueryRepository = taskQueryRepository;
            _mediator = mediator;

        }
        public async Task<string> Handle(ToggleIsCompletedCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{request}");
                // Retrieve the task asynchronously
                var task = await _taskQueryRepository.GetByIdAsync(request.Id);

                if (task == null)
                {
                    _logger.LogError($"Task with ID {request.Id} not found.");
                    throw new NotFoundException($"Task with ID {request.Id} not found.");
                }

                // Toggle the IsCompleted property
                task.IsCompleted = !task.IsCompleted;

                // Update the task
                await _taskCommandRepository.UpdateAsync(task);

                // Create a notification
                CreateNotificationCommand command = new CreateNotificationCommand
                {
                    Type = "COMPLETED",
                    Message = task.IsCompleted ? "Task Completed Successfully" : "Task Marked as Incomplete",
                    UserId = task.UserId
                };

                await _mediator.Send(command, cancellationToken);
                _logger.LogError($"CHANGED SUCCESSFULLY");
                return "CHANGED SUCCESSFULLY";
            }
            catch (NotFoundException ex)
            {
                // Log the exception or handle it accordingly
                // Here, we're rethrowing the exception for the upper layer to handle
                _logger.LogError($"{ex}");
                throw ex;
            }
            catch (Exception ex)
            {
                // Log the exception
                // You might want to provide more specific error messages based on the exception type
                // For simplicity, let's return a general error message
                _logger.LogError($"An error occurred while toggling task completion status. {ex}");
                throw new ApplicationException("An error occurred while toggling task completion status.", ex);
            }
        }
    }

}