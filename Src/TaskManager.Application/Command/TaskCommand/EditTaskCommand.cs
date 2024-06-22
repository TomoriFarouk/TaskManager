using System;
using System.Net.NetworkInformation;
using MediatR;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Command.TaskCommand
{
    //public class EditTaskCommand:IRequest<TaskResponse>
    //{
    //       public int Id { get; set; }
    //       public string Title { get; set; }
    //       public string Description { get; set; }
    //       public DateTime DueDate { get; set; }
    //       public long ProjectId { get; set; }
    //       public string status { get; set; }
    //       public string priority { get; set; }
    //       //public bool IsCompleted { get; set; }
    //       public EditTaskCommand()
    //       {
    //           if (string.IsNullOrEmpty(status))
    //           {
    //               // Handle the case where Status is null or empty (optional)
    //               this.status = "0"; // Or throw an exception if required
    //           }
    //           else
    //           {
    //               this.status = ValidateStatus(status);
    //           }

    //           if (string.IsNullOrEmpty(priority))
    //           {
    //               // Handle the case where Status is null or empty (optional)
    //               this.priority = "0"; // Or throw an exception if required
    //           }
    //           else
    //           {
    //               this.status = ValidatePriority(priority);
    //           }

    //       }

    //       private string ValidateStatus(string status)
    //       {
    //           switch (status.ToLower())
    //           {
    //               case "New":
    //                   return "0";
    //               case "Processing":
    //                   return "1";
    //               case "Completed":
    //                   return "2";
    //               default:
    //                   // Handle unexpected status values
    //                   return "0"; // Or throw an exception if required
    //           }
    //       }



    //       private string ValidatePriority(string priority)
    //       {
    //           switch (priority.ToLower())
    //           {
    //               case "low":
    //                   return "0";
    //               case "medium":
    //                   return "1";
    //               case "high":
    //                   return "2";
    //               default:
    //                   // Handle unexpected status values
    //                   return "0"; // Or throw an exception if required
    //           }
    //       }
    //   }

    public class EditTaskCommand : IRequest<TaskResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public long ProjectId { get; set; }
        public string UserId { get; set; }
        private string _status;
        private string _priority;
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

