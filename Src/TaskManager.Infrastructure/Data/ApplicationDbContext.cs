using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Core.Entities.Identity;

namespace TaskManager.Infrastructure.Data
{
	public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

		}

		public DbSet<Tasks> tasks { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<Notification> notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Tasks>().
                HasOne(t => t.User).
                WithMany(u => u.tasks).
                HasForeignKey(t => t.UserId);

            builder.Entity<Tasks>().
               HasOne(t => t.Project).
               WithMany(p => p.tasks).
               HasForeignKey(t => t.ProjectId);

            base.OnModelCreating(builder);
        }
    }
}

