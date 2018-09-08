using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Hubs
{
    public class ToDoHub : Hub
    {
        public async Task AddTask(string task)
        {
            await Clients.All.SendAsync("TaskAdded", task);
        }
    }
}
