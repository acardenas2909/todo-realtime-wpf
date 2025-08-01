using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WpfTodoApp.Models;

namespace WpfTodoApp.Services
{
    public class TaskApiService
    {
        private readonly HttpClient _http;

        public TaskApiService()
        {
            _http = new HttpClient { BaseAddress = new Uri("https://localhost:44388/") };
        }

        public async Task<List<TaskModel>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<TaskModel>>("api/tasks");

        public async Task<TaskModel> CreateAsync(TaskModel task)
        {
            var res = await _http.PostAsJsonAsync("api/tasks", task);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<TaskModel>();
        }

        public async Task UpdateAsync(TaskModel task)
        {
            var res = await _http.PutAsJsonAsync($"api/tasks/{task.Id}", task);
            res.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/tasks/{id}");
            res.EnsureSuccessStatusCode();
        }

        public async Task LockAsync(int id)
        {
            var res = await _http.PostAsync($"api/tasks/lock/{id}", null);
            res.EnsureSuccessStatusCode();
        }
    }
}


