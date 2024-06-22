using System;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repository.Command.Base;

namespace TaskManager.Infrastructure.Repository.Command
{
	public class TaskCommandRepository:CommandRepository<Tasks>, ITaskCommandRepository
	{
		public TaskCommandRepository(ApplicationDbContext context):base(context)
		{
		}
	}
}

