using System;
using MediatR;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Queries
{
	public class ProjectQueries
	{
        public class GetAllProjectQuery : IRequest<List<Project>>
        {

        }

        //public class GetCustomerByEmailQuery : IRequest<Project>
        //{
        //    public string Email { get; set; }
        //    public GetCustomerByEmailQuery(string email)
        //    {
        //        this.Email = email;
        //    }
        //}

        public class GetProjectByIdQuery : IRequest<Project>
        {
            public Int64 Id { get; set; }
            public GetProjectByIdQuery(Int64 id)
            {
                this.Id = id;
            }
        }
    }
}

