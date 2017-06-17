using System;
using System.Collections.Generic;
using WhatShouldIDoNow.DataAccess.Models;

namespace WhatShouldIDoNow.DataAccess
{
    public interface ITaskCommands
    {
        int CreateTask(CreateTask task);
        TaskToDo GetRandomTaskWithDateStart();
        TaskToDo GetTask(int id);
        void AddCompletedTask(AddCompletedTask task);
        void UpdateTaskDateStart(int id, DateTime dateStart);
        void DeleteTaskTodo(int id);
        List<CompletedTask> GetCompletedTasks();
    }
}
