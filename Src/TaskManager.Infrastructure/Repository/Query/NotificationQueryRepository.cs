using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using TaskManager.Infrastructure.Repository.Query.Base;

namespace TaskManager.Infrastructure.Repository.Query
{
    public class NotificationQueryRepository : QueryRepository<Notification>, INotificationQueryRepository
    {
        public NotificationQueryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        //public async Task<IReadOnlyList<Notification>> GetAllAsync()
        //{
        //    try
        //    {
        //        var query = "SELECT * FROM TASKS";
        //        using (var connection = CreateConnection())
        //        {
        //            return (await connection.QueryAsync<Tasks>(query)).ToList();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        throw new Exception(exp.Message, exp);
        //    }
        //}

        public async Task<Notification> GetByIdAsync(long id)
        {
            try
            {
                var query = "SELECT * FROM NOTIFICATIONS WHERE Id =@Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, System.Data.DbType.Int64);
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<Notification>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

    }
}

