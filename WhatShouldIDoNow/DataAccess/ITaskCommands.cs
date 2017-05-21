using System.Collections.Generic;
using WhatShouldIDoNow.DataAccess.Models;

namespace WhatShouldIDoNow.DataAccess
{
    public interface ITaskCommands
    {
        int CreateTask(CreateTask task);
        TaskToDo GetRandomTask();
        TaskToDo GetTask(int id);
        void AddCompletedTask(AddCompletedTask task);
        void DeleteTaskTodo(int id);
        List<CompletedTask> GetCompletedTasks();
    }
}
