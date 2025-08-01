// Hubs/TaskHub.cs
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using TodoServer.Data;
using TodoServer.Models;

namespace TodoServer.Hubs
{
    public class TaskHub : Hub
    {

        private readonly TodoContext _context;

        public TaskHub()
        {
            _context = new TodoContext();
        }

        public List<TaskEntity> GetAllTasks()
        {
            return _context.Tasks.OrderBy(t => t.Priority).ToList();
        }

        public void NotifyChange(string type, object task)
        {
            Clients.All.broadcastChange(type, task);
        }

    }
}
