using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SignalRServer.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        public static List<string> tasks = new List<string>()
        {
            "Clean room", "Clean web browser history", "Feed cat", "Go to gym", "Cook dinner"
        };

        [HttpGet("[action]")]
        public IEnumerable<dynamic> Tasks()
        {
            return tasks.Select(task => new
            {
                id = Guid.NewGuid().ToString(),
                title = task
            });
        }
    }
}