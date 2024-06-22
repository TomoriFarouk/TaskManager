using System;
using MediatR;

namespace TaskManager.Application.Command.ProjectCommand
{
	public class DeleteProjectCommand:IRequest<String>
	{
        public int Id { get; set; }

        public DeleteProjectCommand(int Id)
        {
            this.Id = Id;
        }
    }
}

