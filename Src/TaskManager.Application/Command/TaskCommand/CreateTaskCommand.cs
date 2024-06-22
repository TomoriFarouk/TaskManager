using System;
using MediatR;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Command.TaskCommand
{
    public class CreateTaskCommand : IRequest<TaskResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string UserId { get; set; }
        public long ProjectId { get; set; }
        private string _status;
        private string _priority;
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public string status
        {
            get => _status;
            set => _status = ValidateStatus(value);
        }

        public string priority
        {
            get => _priority;
            set => _priority = ValidatePriority(value);
        }
        public CreateTaskCommand()
        {
            this.IsCompleted = false;
            this.CreatedDate = DateTime.Now;
           

        }

        private string ValidateStatus(string status)
        {
            if (Enum.TryParse(typeof(Status), status, true, out var result))
            {
                return ((Status)result).ToString();
            }
            else
            {
                return Status.Pending.ToString(); // Or throw an exception if required
            }
        }

        private string ValidatePriority(string priority)
        {
            if (Enum.TryParse(typeof(Priority), priority, true, out var result))
            {
                return ((Priority)result).ToString();
            }
            else
            {
                return Priority.Low.ToString(); // Or throw an exception if required
            }
        }
    }

}
