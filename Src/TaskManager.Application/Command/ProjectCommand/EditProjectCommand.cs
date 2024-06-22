using System;
using MediatR;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Command.ProjectCommand
{
	public class EditProjectCommand:IRequest<ProjectResponse>
	{
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
       
    }
}

