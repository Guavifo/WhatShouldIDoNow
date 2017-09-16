using Dapper;
using System;
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
                    ([Description], IntervalByHour, UserID)
                    values (@Description, @IntervalByHour, @UserId)
                    select SCOPE_IDENTITY()";

                var id = conn.ExecuteScalar<int>(sql, task);
                return id;
            }
        }

        public TaskToDo GetRandomTaskWithDateStart(int userId)
        {
            //Get a random task that started in the past
            string sql;
            sql = @"SELECT TOP 1 [Id],[DateCreated],[Description],[Category],[DateDue],[LastViewed],
                    [DateStart],[TimesViewed],[IntervalByHour] FROM [dbo].[TasksToDo]
                    Where getdate() > [DateStart] and UserID = @userId
                    Order by NEWID()";
            using (IDbConnection conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var task = conn.QuerySingleOrDefault<TaskToDo>(sql, new { userId = userId });
                return task;

            }
        }

        public List<AllActiveTask> GetAllActiveTask(int userId)
        {
            //View all active current and future tasks
            string sql;
            sql = @"SELECT [Id],[DateCreated],[Description],[Category],[DateDue],[LastViewed],
                    [DateStart],[TimesViewed],[IntervalByHour] FROM [dbo].[TasksToDo]
                    Where getdate() > [DateStart] and UserID = @userId
                    Order by [DateStart]";
            using (IDbConnection conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var results = conn.Query<AllActiveTask>(sql, new { userId = userId });
                return results.ToList();

            }
        }

        public TaskToDo GetTask(int id, int userId)
        {
            string sql;
            sql = "SELECT TOP 1 [Id],[DateCreated],[Description],[Category],[DateDue],[LastViewed]," +
                "[DateStart],[TimesViewed],[IntervalByHour] FROM [dbo].[TasksToDo] where Id = @Id and UserID = @userId";
            using (IDbConnection conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var task = conn.QuerySingleOrDefault<TaskToDo>(sql, new { Id = id, userId = userId });
                return task;
            }
        }

        public void UpdateTaskDateStart(int id, DateTime dateStart, int userId)
        {
            string sql;
            sql = "UPDATE[dbo].[TasksToDo] Set DateStart = @dateStart Where ID = @id and UserID = @userId";
            using (IDbConnection conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                conn.Execute(sql, new { id, dateStart, userId });
            }
        }

        public void AddCompletedTask(AddCompletedTask task)
        {
            var sql = @"
                insert into TasksCompleted
                ([Description], DateCreated, Category, UserID)
                values (@Description, @DateCreated, @Category, @UserId)";
            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                conn.Execute(sql, task);
            }
        }

        public void DeleteTaskTodo(int id, int userId)
        {
            var sql = "delete from TasksTodo where Id = @Id and userId = @userId";
            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                conn.Execute(sql, new { Id = id, userId = userId });
            }
        }

        public List<CompletedTask> GetCompletedTasks(int userId)
        {
            var sql = "SELECT [Id],[DateCreated],Description,[Category],[DateCompleted] FROM [dbo].[TasksCompleted] Where UserID = @userId";
            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var results = conn.Query<CompletedTask>(sql, new { userId = userId });
                return results.ToList();
            }
        }
    }
}
