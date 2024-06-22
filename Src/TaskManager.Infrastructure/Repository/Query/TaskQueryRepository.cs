using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using TaskManager.Infrastructure.Repository.Query.Base;

namespace TaskManager.Infrastructure.Repository.Query
{
    public class TaskQueryRepository : QueryRepository<Tasks>, ITaskQueryRepository
    {
        public TaskQueryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IReadOnlyList<Tasks>> GetAllAsync()
        {
            try
            {
                var query = "SELECT * FROM TASKS";
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<Tasks>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Tasks> GetByIdAsync(long id)
        {
            try
            {
                var query = "SELECT * FROM TASKS WHERE Id =@Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, System.Data.DbType.Int64);
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<Tasks>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }


        public async Task<Tasks> GetByStatusAsync(string status)
        {
            try
            {
                var query = "SELECT * FROM TASKS WHERE status =@status";
                var parameters = new DynamicParameters();
                parameters.Add("status", status, System.Data.DbType.Int64);
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<Tasks>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Tasks> GetByPriorityAsync(string priority)
        {
            try
            {
                var query = "SELECT * FROM TASKS WHERE priority =@priority";
                var parameters = new DynamicParameters();
                parameters.Add("priority",priority, System.Data.DbType.Int64);
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<Tasks>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        //public async Task<Customer> GetCustomerByEmail(string email)
        //{
        //    try
        //    {
        //        var query = "SELECT * FROM CUSTOMER WHERE EMAIL=@email";
        //        var parameters = new DynamicParameters();
        //        parameters.Add("Email", email, System.Data.DbType.String);
        //        using (var connection = CreateConnection())
        //        {
        //            return (await connection.QueryFirstOrDefaultAsync<Customer>(query, parameters));
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        throw new Exception(exp.Message, exp);
        //    }
        //}
    }
}

