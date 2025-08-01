using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfTodoApp.Models;

namespace WpfTodoApp.Services
{
    public class SignalRService
    {
        private HubConnection _connection;
        private IHubProxy _hub;

        public SignalRService()
        {
            _connection = new HubConnection("https://localhost:44388/");
            _hub = _connection.CreateHubProxy("TaskHub");
            _connection.Start().Wait();
        }


        public void SubscribeToChanges(Action<string, object> onChange)
        {
            _hub.On("broadcastChange", (string changeType, object task) =>
            {
                onChange?.Invoke(changeType, task);
            });
        }

        public async Task NotifyChange(string type, object task)
        {
            await _hub.Invoke("NotifyChange", type, task);
        }

        public async Task<List<TaskModel>> GetAllTasksAsync() =>
            await _hub.Invoke<List<TaskModel>>("GetAllTasks");

        public async Task UpdateTaskAsync(TaskModel task) =>
            await _hub.Invoke("UpdateTask", task);

        public async Task LockTaskAsync(int taskId) =>
            await _hub.Invoke("LockTask", taskId);

        public async Task DeleteTaskAsync(TaskModel task) =>
            await _hub.Invoke("DeleteTask", task);


    }

}
