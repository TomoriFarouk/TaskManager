using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Data
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public NotificationBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationCommandRepository>();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    await CheckDueTasks(notificationService, dbContext);

                    await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);  // Adjust the interval as needed
                }
            }
        }

        private async Task CheckDueTasks(INotificationCommandRepository notificationService, ApplicationDbContext dbContext)
        {

            // Check for tasks due within the next 48 hours
            var now = DateTime.Now;
            var dueTasks = await dbContext.tasks
               .Where(t => t.DueDate <= now.AddHours(48) && t.DueDate > now && !t.IsCompleted)
                .ToListAsync();

            foreach (var task in dueTasks)
            {
                var dueDateNotification = new Notification
                {
                    Type = "DueDateReminder",
                    Message = $"Task '{task.Title}' is due soon.",
                    Timestamp = DateTime.Now,
                    UserId = task.UserId
                };

                await notificationService.AddAsync(dueDateNotification);
            }


            // Check for overdue tasks
            var overdueTasks = await dbContext.tasks
                .Where(t => t.DueDate < now && !t.IsCompleted)
                .ToListAsync();

            foreach (var task in overdueTasks)
            {
                var overdueNotification = new Notification
                {
                    Type = "OverdueTask",
                    Message = $"Task '{task.Title}' created on '{task.CreatedDate}' is overdue.",
                    Timestamp = now,
                    UserId = task.UserId
                };

                await notificationService.AddAsync(overdueNotification);
            }
        }
    }
}



