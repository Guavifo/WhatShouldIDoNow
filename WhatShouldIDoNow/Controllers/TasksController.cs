using Microsoft.AspNetCore.Mvc;
using System;
using WhatShouldIDoNow.DataAccess;
using WhatShouldIDoNow.DataAccess.Models;
using WhatShouldIDoNow.Models;
using Microsoft.AspNetCore.Authorization;
using WhatShouldIDoNow.Services;

namespace WhatShouldIDoNow.Controllers
{
    [Authorize]

    public class TasksController : Controller
    {
        private int _userId;
        private readonly ITaskCommands _taskCommands;
        private const string _homeRedirectUrl = "~/Home/Index";

        public TasksController(ITaskCommands taskCommands, ISecurityService securityService)
        {
            _taskCommands = taskCommands;
            _userId = securityService.GetCurrentUserId();

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
                    IntervalByHour = model.IntervalByHour,
                    UserId = _userId
                });

            return new LocalRedirectResult(_homeRedirectUrl);
        }

        public IActionResult Random()
        {
            var task = _taskCommands.GetRandomTaskWithDateStart(_userId);  
            return View(task);
        }

        [HttpPost]
        public IActionResult Complete(int id)
        {
            var taskToComplete = _taskCommands.GetTask(id, _userId);

            if (taskToComplete == null)
            {
                return new NotFoundResult();
            }

            //Save the completed task to the completed tasks table
            var completedTask = new AddCompletedTask
            {
                Description = taskToComplete.Description,
                DateCreated = taskToComplete.DateCreated,
                Category = taskToComplete.Category,
                UserId = _userId
            };

            _taskCommands.AddCompletedTask(completedTask);

            //Check if the task is repeating, if not, delete it
            if (taskToComplete.IntervalByHour == 0)
            {
                _taskCommands.DeleteTaskTodo(id, _userId);
            }
            //if the task IS repeating, update the DateStart field to the future (vs. right now)
            else
            {
                _taskCommands
                    .UpdateTaskDateStart(
                        id, 
                        DateTime.Now.AddHours(taskToComplete.IntervalByHour),
                        _userId
                    );
            }

            return new LocalRedirectResult(_homeRedirectUrl);
        }


        //This code snoozes a task for 5 minutes

        [HttpPost]
        public IActionResult Snooze(int id)
        {
            var taskToSnooze = _taskCommands.GetTask(id, _userId);

            if (taskToSnooze == null)
            {
                return new NotFoundResult();
            }

            //Update the DateStart field to the future (vs. right now)
            else
            {
                _taskCommands
                    .UpdateTaskDateStart(
                        id,
                        DateTime.Now.AddMinutes(5),
                        _userId
                    );
            }

            return new LocalRedirectResult(_homeRedirectUrl);
        }






        //This displayes the completed list of tasks

        public IActionResult CompletedList()
        {
            var model = _taskCommands.GetCompletedTasks(_userId);
            return View(model);
        }
    }
}