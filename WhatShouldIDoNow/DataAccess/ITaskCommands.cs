using WhatShouldIDoNow.DataAccess.Models;

namespace WhatShouldIDoNow.DataAccess
{
    public interface ITaskCommands
    {
        int CreateTask(CreateTask task);
        TaskToDo GetRandomTask();
    }
}
