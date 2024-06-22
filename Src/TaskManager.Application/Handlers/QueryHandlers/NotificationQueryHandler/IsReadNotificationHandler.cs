using System;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.NotificationCommand;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Core.Interface.Command;
using TaskManager.Core.Interface.Query;

namespace TaskManager.Application.Handlers.QueryHandlers.NotificationHandler
{
    public class IsReadNotificationHandler : IRequestHandler<IsReadCommand, string>
    {
        private readonly INotificationCommandRepository _notificationCommandRepository;
        private readonly INotificationQueryRepository _notificationQueryRepository;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public IsReadNotificationHandler(INotificationCommandRepository notificationRepository,
            INotificationQueryRepository notificationQueryRepository, IMediator mediator,ILogger logger
            )
        {
            _notificationCommandRepository = notificationRepository;
            _notificationQueryRepository = notificationQueryRepository;
            _mediator = mediator;
            _logger = logger;

        }
        public async Task<string> Handle(IsReadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{request}");
                // Retrieve the task asynchronously
                var notification = await _notificationQueryRepository.GetByIdAsync(request.Id);

                if (notification == null)
                {
                    _logger.LogError($"Task with ID {request.Id} not found.");
                    throw new NotFoundException($"Task with ID {request.Id} not found.");
                }

                // Toggle the IsCompleted property
                notification.IsReadStatus = !notification.IsReadStatus;

                // Update the task
                await _notificationCommandRepository.UpdateAsync(notification);
                _logger.LogError($"CHANGED SUCCESSFULLY");
                return "CHANGED SUCCESSFULLY";
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"{ex.Message}");
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while toggling task completion status.{ex.Message}");
                throw new ApplicationException("An error occurred while toggling task completion status.", ex);
            }
        }
    }
}

