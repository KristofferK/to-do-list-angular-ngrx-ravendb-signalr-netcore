using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SignalRServer.Models;

namespace SignalRServer.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        public static List<Task> tasks = new List<Task>()
        {
            new Task()
            {
                Title = "Clean room",
                Id = Guid.NewGuid().ToString(),
                Completed = true
            },
            new Task()
            {
                Title = "Feed cat",
                Id = Guid.NewGuid().ToString(),
                Completed = true
            },
            new Task()
            {
                Title = "Go to gym",
                Id = Guid.NewGuid().ToString(),
                Completed = false
            }
        };

        [HttpGet("[action]")]
        public IEnumerable<Task> Tasks()
        {
            return tasks;
        }
    }
}