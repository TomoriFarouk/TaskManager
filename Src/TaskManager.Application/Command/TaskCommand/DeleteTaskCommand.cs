using System;
using MediatR;

namespace TaskManager.Application.Command.TaskCommand
{
	public class DeleteTaskCommand:IRequest<String>
	{
        public int Id { get; set; }

        public DeleteTaskCommand(int Id)
        {
            this.Id = Id;
        }
    }
}

