using System;
using System.Collections.Generic;
using WhatShouldIDoNow.DataAccess.Models;

namespace WhatShouldIDoNow.DataAccess
{
    public interface ITaskCommands
    {
        int CreateTask(CreateTask task);
        TaskToDo GetRandomTaskWithDateStart(int userId);
        TaskToDo GetTask(int id, int userId);
        void AddCompletedTask(AddCompletedTask task);
        void UpdateTaskDateStart(int id, DateTime dateStart, int userId);
        void DeleteTaskTodo(int id, int userId);
        List<CompletedTask> GetCompletedTasks(int userId);
    }
}
