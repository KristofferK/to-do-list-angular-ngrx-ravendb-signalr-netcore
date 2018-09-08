using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Models
{
    public class Task
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public bool Completed { get; set; }
    }
}
