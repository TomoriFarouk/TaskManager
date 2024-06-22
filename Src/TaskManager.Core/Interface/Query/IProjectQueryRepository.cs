using System;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query.Base;

namespace TaskManager.Core.Interface.Query
{
    public interface IProjectQueryRepository : IQueryRepository<Project>
    {
        Task<IReadOnlyList<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(Int64 id);
        //Task<Customer> GetCustomerByEmail(string email);
    }
}

