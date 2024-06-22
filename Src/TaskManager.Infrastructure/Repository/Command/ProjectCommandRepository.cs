using System;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Command;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repository.Command.Base;

namespace TaskManager.Infrastructure.Repository.Command
{
    public class ProjectCommandRepository : CommandRepository<Project>, IProjectCommandRepository
    {
        public ProjectCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

