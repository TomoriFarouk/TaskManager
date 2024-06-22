using System;
using TaskManager.Core.Entities.Base;

namespace TaskManager.Core.Entities
{
	public class Project:BaseEntity
	{
		public int Id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
        public ICollection<Tasks> tasks { get; set; }
    }
}

