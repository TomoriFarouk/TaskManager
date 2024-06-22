using System;
using MediatR;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Queries
{
	public class TaskQueries
	{
        public class GetAllTaskQuery : IRequest<List<Tasks>>
        {

        }

        public class GetTaskByIdQuery : IRequest<Tasks>
        {
            public Int64 Id { get; set; }
            public GetTaskByIdQuery(Int64 id)
            {
                this.Id = id;
            }
        }

        public class GetTaskByStatusQuery : IRequest<Tasks>
        {
            public string status { get; set; }
            public GetTaskByStatusQuery(string status)
            {
                this.status = status;
            }
        }

        public class GetTaskByPriorityQuery : IRequest<Tasks>
        {
            public string priority { get; set; }
            public GetTaskByPriorityQuery(string priority)
            {
                this.priority = priority;
            }
        }

        public class GetAllTaskDueForTheWeekQuery : IRequest<List<Tasks>>
        {

        }

    }
}

