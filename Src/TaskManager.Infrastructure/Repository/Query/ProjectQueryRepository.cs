using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaskManager.Core.Entities;
using TaskManager.Core.Interface.Query;
using TaskManager.Infrastructure.Repository.Query.Base;

namespace TaskManager.Infrastructure.Repository.Query
{
    public class ProjectQueryRepository : QueryRepository<Project>, IProjectQueryRepository
    {
        public ProjectQueryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IReadOnlyList<Project>> GetAllAsync()
        {
            try
            {
                var query = "SELECT * FROM PROJECTS";
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<Project>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Project> GetByIdAsync(long id)
        {
            try
            {
                var query = "SELECT * FROM PROJECTS WHERE Id =@Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, System.Data.DbType.Int64);
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<Project>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }


        
    }
}

