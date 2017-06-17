using Microsoft.AspNetCore.Mvc;
using WhatShouldIDoNow.DataAccess;
using WhatShouldIDoNow.DataAccess.Models;
using WhatShouldIDoNow.Models;

namespace WhatShouldIDoNow.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskCommands _taskCommands;
        private const string _homeRedirectUrl = "~/Home/Index";

        public TasksController(ITaskCommands taskCommands)
        {
            _taskCommands = taskCommands;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                      
            //Check if the user has clicked the custom interval button
            if (model.ButtonInterval > -1)
            {
                model.IntervalByHour = model.ButtonInterval;
            }

            var taskId = _taskCommands.CreateTask(
                new CreateTask
                {
                    Description = model.Description,
                    IntervalByHour = model.IntervalByHour
                });

            return new LocalRedirectResult(_homeRedirectUrl);
        }

        public IActionResult Random()
        {
            var task = _taskCommands.GetRandomTask();
            return View(task);
        }

        [HttpPost]
        public IActionResult Complete(int id)
        {
            var taskToComplete = _taskCommands.GetTask(id);
            if (taskToComplete == null)
            {
                return new NotFoundResult();
            }

            var completedTask = new AddCompletedTask
            {
                Description = taskToComplete.Description,
                DateCreated = taskToComplete.DateCreated,
                Category = taskToComplete.Category
            };

            _taskCommands.AddCompletedTask(completedTask);
            _taskCommands.DeleteTaskTodo(id);

            return new LocalRedirectResult(_homeRedirectUrl);
        }
        
        public IActionResult CompletedList()
        {
            var model = _taskCommands.GetCompletedTasks();
            return View(model);
        }
    }
}