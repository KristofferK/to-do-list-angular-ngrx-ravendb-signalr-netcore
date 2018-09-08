using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Database;

namespace SignalRServer.Hubs
{
    public class ToDoHub : Hub
    {
        private static DatabaseAccessLayer database = new DatabaseAccessLayer();
        public async Task AddTask(string taskTitle)
        {
            var task = new Models.Task()
            {
                Id = Guid.NewGuid().ToString(),
                Title = taskTitle,
                Completed = false
            };

            Controllers.TasksController.tasks.Add(task);
            database.Add(task);
            await Clients.All.SendAsync("TaskAdded", task);
        }
    }
}
