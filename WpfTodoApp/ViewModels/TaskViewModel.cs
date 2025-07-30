using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfTodoApp.Models;
using WpfTodoApp.Services;

namespace WpfTodoApp.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }
        public ICommand LoadTasksCommand { get; set; }
        public ICommand SaveTaskCommand { get; set; }
        public ICommand LockTaskCommand { get; set; }

        private readonly SignalRService _signalR;

        public TaskViewModel()
        {
            Tasks = new ObservableCollection<TaskModel>();
            _signalR = new SignalRService();

            LoadTasksCommand = new RelayCommand(async () => await LoadTasks());
            SaveTaskCommand = new RelayCommand<TaskModel>(async param => await SaveTask(param));
            LockTaskCommand = new RelayCommand<TaskModel>(async param => await LockTask(param)); 
        }

        private async Task LoadTasks()
        {
            var loaded = await _signalR.GetAllTasksAsync();
            Tasks.Clear();
            foreach (var t in loaded)
                Tasks.Add(t);
        }

        private async Task SaveTask(TaskModel task)
        {
            await _signalR.UpdateTaskAsync(task);
        }

        private async Task LockTask(TaskModel task)
        {
            await _signalR.LockTaskAsync(task.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
