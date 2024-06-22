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
using TaskManager.Core.Interface.Logger;
using TaskManager.Core.Interface.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManager.Application.Handlers.CommandHandlers.TaskHandler
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand,TaskResponse>
    {
        private readonly ITaskCommandRepository _taskCommandRepository;
        private readonly IIdentityService _identityService;
        private readonly IProjectQueryRepository _projectRepository;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CreateTaskHandler(ILogger logger, IMediator mediator,ITaskCommandRepository taskCommandRepository, IIdentityService identityService, IProjectQueryRepository projectQueryRepository)
        {
            _taskCommandRepository = taskCommandRepository;
            _identityService = identityService;
            _projectRepository = projectQueryRepository;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<TaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{request}");
                var taskEntity = TaskManagerMapper.Mapper.Map<Tasks>(request);

                var userExists = await _identityService.GetUserByIdAsync(request.UserId);
                if (userExists is null)
                {
                    _logger.LogError($"User does not exist");
                    throw new Exception("User does not exist");
                }
                var projectExists = await _projectRepository.GetByIdAsync(request.ProjectId);
                //if (projectExists is null)
                //{
                //    throw new Exception("Project does not exist");
                //}
                if (taskEntity is null)
                {
                    _logger.LogError($"There is a problem in mapper");
                    throw new ApplicationException("There is a problem in mapper");
                }

                var newCustomer = await _taskCommandRepository.AddAsync(taskEntity);
                var customerResponse = TaskManagerMapper.Mapper.Map<TaskResponse>(newCustomer);
                CreateNotificationCommand command = new CreateNotificationCommand
                {
                    Type = "COMPLETED",
                    Message = "Task CREATED Successfully",
                    UserId = request.UserId
                };

                await _mediator.Send(command, cancellationToken);
                _logger.LogError($"{customerResponse}");
                return customerResponse;
            }catch(DbUpdateException ex)
            {
                var sqliteException = ex.InnerException as SqliteException;
                if (sqliteException != null && sqliteException.SqliteErrorCode == 19)
                {
                    _logger.LogError($"Foreign key constraint violation: referenced user or project does not exist. {sqliteException}");
                    throw new Exception("Foreign key constraint violation: referenced user or project does not exist.", sqliteException);
                }

                throw;
            }
        }
    }
}

