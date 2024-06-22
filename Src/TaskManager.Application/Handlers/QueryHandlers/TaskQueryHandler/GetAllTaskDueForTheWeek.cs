using System;
using MediatR;
using static TaskManager.Application.Queries.TaskQueries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Handlers.QueryHandlers.TaskQueryHandler
{
    public class GetAllTaskDueForTheWeek : IRequestHandler<GetAllTaskDueForTheWeekQuery, List<Tasks>>
    {
        private readonly IMediator _mediator;
        private readonly ITaskQueryRepository _taskQueryRepository;
        private readonly ILogger _logger;

        public GetAllTaskDueForTheWeek(IMediator mediator, ITaskQueryRepository taskQueryRepository,ILogger logger)
        {
            _mediator = mediator;
            _taskQueryRepository = taskQueryRepository;
            _logger = logger;
        }
        public async Task<List<Tasks>> Handle(GetAllTaskDueForTheWeekQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            var task = await _mediator.Send(new GetAllTaskQuery());
            var startOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);
            var selectedTask = task.Where(t => t.DueDate >= startOfWeek && t.DueDate < endOfWeek)
                                  .ToList();

            return selectedTask;
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}

