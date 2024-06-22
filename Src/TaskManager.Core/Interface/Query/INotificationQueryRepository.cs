using System;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query.Base;

namespace TaskManager.Core.Interface.Query
{
    public interface INotificationQueryRepository : IQueryRepository<Notification>
    {
        
        Task<Notification> GetByIdAsync(Int64 id);
        

    }
}

