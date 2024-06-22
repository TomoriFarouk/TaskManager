using System;
using MediatR;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Command.ProjectCommand
{
	public class CreateProjectCommand:IRequest<ProjectResponse>
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime CreatedDate { get; set; }
        public CreateProjectCommand()
		{
            this.CreatedDate = DateTime.Now;
        }
	}
}

