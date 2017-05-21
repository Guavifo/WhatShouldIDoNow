using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public TaskToDo GetRandomTask()
        {
            string sql;
            sql = "SELECT TOP 1 [Id],[DateCreated],[Description],[Category],[DateDue],[LastViewed],[DateStart],[TimesViewed],[IntervalByHour] FROM [WsidnData].[dbo].[TasksToDo] Order by NEWID()";
            using (IDbConnection conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var task = conn.QuerySingleOrDefault<TaskToDo>(sql);
                return task;

            }
        }

        public TaskToDo GetTask(int id)
        {
            string sql;
            sql = "SELECT TOP 1 [Id],[DateCreated],[Description],[Category],[DateDue],[LastViewed],[DateStart],[TimesViewed],[IntervalByHour] FROM [WsidnData].[dbo].[TasksToDo] where Id = @Id";
            using (IDbConnection conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var task = conn.QuerySingleOrDefault<TaskToDo>(sql, new { Id = id });
                return task;
            }
        }

        public void AddCompletedTask(AddCompletedTask task)
        {
            var sql = @"
                insert into TasksCompleted
                ([Description], DateCreated, Category)
                values (@Description, @DateCreated, @Category)";
            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                conn.Execute(sql, task);
            }
        }

        public void DeleteTaskTodo(int id)
        {
            var sql = "delete from TasksTodo where Id = @Id";
            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public List<CompletedTask> GetCompletedTasks()
        {
            var sql = "SELECT [Id],[DateCreated],Description,[Category],[DateCompleted] FROM [WsidnData].[dbo].[TasksCompleted]";
            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var results = conn.Query<CompletedTask>(sql);
                return results.ToList();
            }
        }
    }
}
