using System;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.NotificationCommand;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Application.Common.Interface;
using TaskManager.Application.Mapper;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command;
using TaskManager.Core.Interface.Query;

namespace TaskManager.Application.Handlers.CommandHandlers.NotificationHandler
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, string>
    {
        private readonly INotificationCommandRepository _notificationCommandRepository;
        private readonly IIdentityService _identityService;
        private readonly ILogger _logger;

        public CreateNotificationHandler(INotificationCommandRepository notificationCommandRepository, IIdentityService identityService,ILogger logger)
        {
            _notificationCommandRepository = notificationCommandRepository;
            _identityService = identityService;
            _logger = logger;
        }
        public async Task<string> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{request}");
                var notificationEntity = TaskManagerMapper.Mapper.Map<Notification>(request);

                
                if (notificationEntity is null)
                {
                    _logger.LogError($"There is a problem in mapper");
                    throw new ApplicationException("There is a problem in mapper");
                }

                var newCustomer = await _notificationCommandRepository.AddAsync(notificationEntity);
                _logger.LogError($"Notification Created Successfully");
                return "Notification Created Successfully";

            }
            catch (DbUpdateException ex)
            {
                var sqliteException = ex.InnerException as SqliteException;
                if (sqliteException != null && sqliteException.SqliteErrorCode == 19)
                {
                    _logger.LogError($"Foreign key constraint violation: referenced user or project does not exist.{sqliteException}");
                    throw new Exception("Foreign key constraint violation: referenced user or project does not exist.", sqliteException);
                }

                throw;
            }
        }
    }
}

