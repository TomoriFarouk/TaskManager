using System;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query.Base;

namespace TaskManager.Core.Interface.Query
{
    public interface ITaskQueryRepository : IQueryRepository<Tasks>
    {
        Task<IReadOnlyList<Tasks>> GetAllAsync();
        Task<Tasks> GetByIdAsync(Int64 id);
        Task<Tasks> GetByStatusAsync(string status);
        Task<Tasks> GetByPriorityAsync(string priority);

    }
}

