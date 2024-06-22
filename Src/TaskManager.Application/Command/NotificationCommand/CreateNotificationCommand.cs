using System;
using MediatR;
using TaskManager.Application.Response;

namespace TaskManager.Application.Command.NotificationCommand
{
    public class CreateNotificationCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public bool IsReadstatus { get; set; }
        public CreateNotificationCommand()
        {
            this.Timestamp = DateTime.Now;
            this.IsReadstatus = false;
        }
    }
}

