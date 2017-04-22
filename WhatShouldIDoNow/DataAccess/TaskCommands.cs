using Dapper;
using WhatShouldIDoNow.DataAccess.Models;

namespace WhatShouldIDoNow.DataAccess
{
    public class TaskCommands : ITaskCommands
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public TaskCommands(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public int CreateTask(CreateTask task)
        {
            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var sql = @"
                    insert into TasksToDo
                    ([Description], IntervalByHour)
                    values (@Description, @IntervalByHour)
                    select SCOPE_IDENTITY()";

                var id = conn.ExecuteScalar<int>(sql, task);
                return id;
            }
        }
    }
}
