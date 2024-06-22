using System;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command.Base;

namespace TaskManager.Core.Interface.Command
{
	public interface ITaskCommandRepository :ICommandRepository<Tasks>
	{
		
	}
}

