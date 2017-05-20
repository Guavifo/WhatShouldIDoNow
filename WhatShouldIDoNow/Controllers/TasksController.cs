using Microsoft.AspNetCore.Mvc;
using WhatShouldIDoNow.DataAccess;
using WhatShouldIDoNow.DataAccess.Models;
using WhatShouldIDoNow.Models;

namespace WhatShouldIDoNow.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskCommands _taskCommands;

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

            var taskId =_taskCommands.CreateTask(
                new CreateTask
                {
                    Description = model.Description,
                    IntervalByHour = model.IntervalByHour
                });

            return View(model);
        }

        public IActionResult RandomTask()
        {
            var task = _taskCommands.GetRandomTask();
            return View(task);

        }

        
    }
}