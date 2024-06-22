using System;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using TaskManager.Infrastructure.Data;
using TaskManager.Core.Interface.Query.Base;

namespace TaskManager.Infrastructure.Repository.Query.Base
{
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : class
    {
        public QueryRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}

