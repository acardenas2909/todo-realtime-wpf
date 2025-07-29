// Hubs/TaskHub.cs
using Microsoft.AspNet.SignalR;

namespace TodoServer.Hubs
{
    public class TaskHub : Hub
    {
        public void NotifyChange(string type, object task)
        {
            Clients.All.broadcastChange(type, task);
        }
    }
}
