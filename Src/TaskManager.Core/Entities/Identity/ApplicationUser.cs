using System;
using Microsoft.AspNetCore.Identity;
namespace TaskManager.Core.Entities.Identity
{
	public class ApplicationUser:IdentityUser
	{
		public string? FullName { get; set; }
        public ICollection<Tasks> tasks { get; set; }

    }
}

