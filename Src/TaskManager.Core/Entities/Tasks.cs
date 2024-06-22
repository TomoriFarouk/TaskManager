using System;
using System.ComponentModel.DataAnnotations;
using TaskManager.Core.Entities.Base;
using TaskManager.Core.Entities.Identity;

namespace TaskManager.Core.Entities
{
    public class Tasks : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        [Required]
        public Status status { get; set; }
        [Required]
        public Priority priority { get; set; }
        public bool IsCompleted { get; set; }
    }
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        Pending,
        InProgress,
        Completed
    }
}

