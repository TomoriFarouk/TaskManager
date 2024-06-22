using System;
using TaskManager.Core.Entities.Identity;

namespace TaskManager.Core.Entities
{
	public class Notification
	{
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool IsReadStatus { get; set; }

    }
}

