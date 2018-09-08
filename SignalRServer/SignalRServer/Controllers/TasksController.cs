using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SignalRServer.Database;
using SignalRServer.Models;

namespace SignalRServer.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {

        private static DatabaseAccessLayer database = new DatabaseAccessLayer();

        [HttpGet("[action]")]
        public IEnumerable<Task> Tasks()
        {
            return database.GetTasks();
        }
    }
}