using System;
namespace TaskManager.Application.Response
{
	public class TaskResponse
	{

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string UserId { get; set; }
        public long ProjectId { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public bool IsCompleted { get; set; }

    }
}

