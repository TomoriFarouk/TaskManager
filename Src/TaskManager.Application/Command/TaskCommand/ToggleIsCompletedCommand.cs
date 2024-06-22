using System;
using MediatR;

namespace TaskManager.Application.Command.TaskCommand
{
	public class ToggleIsCompletedCommand:  IRequest<String>
	{
        public int Id { get; set; }

        public ToggleIsCompletedCommand(int Id)
        {
            this.Id = Id;
        }

    }
}

