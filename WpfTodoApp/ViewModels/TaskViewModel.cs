using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfTodoApp.Models;
using WpfTodoApp.Services;

namespace WpfTodoApp.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private readonly SignalRService _signalR;
        private readonly TaskApiService _taskApi;

        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<TaskModel> Tasks { get; set; }

        public ICommand AddNewTaskCommand { get; }
        public ICommand LoadTasksCommand { get; }
        public ICommand SaveTaskCommand { get; }
        public ICommand LockTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        private TaskModel _newTask = new TaskModel();
        public TaskModel NewTask
        {
            get => _newTask;
            set
            {
                _newTask = value;
            }
        }
        private bool _showAddForm;
        public bool ShowAddForm
        {
            get => _showAddForm;
            set
            {
                _showAddForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isAddingTask;
        public bool IsAddingTask
        {
            get => _isAddingTask;
            set
            {
                if (_isAddingTask != value)
                {
                    _isAddingTask = value;
                    OnPropertyChanged(nameof(IsAddingTask));
                }
            }
        }


        public RelayCommand ToggleAddFormCommand { get; set; }

        public TaskViewModel()
        {
            Tasks = new ObservableCollection<TaskModel>();
            _signalR = new SignalRService();
            _taskApi = new TaskApiService();

            LoadTasksCommand = new RelayCommand(async () => await LoadTasks());
            SaveTaskCommand = new RelayCommand<TaskModel>(async param => await SaveTask(param));
            LockTaskCommand = new RelayCommand<TaskModel>(async param => await LockTask(param));
            DeleteTaskCommand = new RelayCommand<TaskModel>(async param => await DeleteTask(param));
            AddNewTaskCommand = new RelayCommand(async () => await AddTaskAsync());
            ToggleAddFormCommand = new RelayCommand(() => ShowAddForm = !ShowAddForm);
            _ = InitializeAsync();
        }

    
        private async Task InitializeAsync()
        {
            await LoadTasks();

            _signalR.SubscribeToChanges(async (type, taskObj) =>
            {
                await LoadTasks(); // actualiza lista cuando se detectan cambios
            });
        }

        private async Task LoadTasks()
        {
            var loaded = await _taskApi.GetAllAsync();
            App.Current.Dispatcher.Invoke(() =>
            {
                Tasks.Clear();
                foreach (var task in loaded)
                    Tasks.Add(task);
            });
        }

        private async Task AddTaskAsync()
        {            

            try
            {
                var createdTask = await _taskApi.CreateAsync(NewTask);
                // Limpiar campos
                NewTask = new TaskModel();
                OnPropertyChanged(nameof(NewTask));

                // Ocultar formulario
                ShowAddForm = false;
            }
            catch (Exception ex)
            {
                // Aquí podrías notificar al usuario en la UI
                Console.WriteLine("Error al crear la tarea: " + ex.Message);
            }
        }

        private async Task SaveTask(TaskModel task)
        {
            if (task.Id == 0)
            {
                var created = await _taskApi.CreateAsync(task);
                await _signalR.NotifyChange("create", created);
            }
            else
            {
                await _taskApi.UpdateAsync(task);
                await _signalR.NotifyChange("update", task);
            }
            await LoadTasks();
        }

        private async Task DeleteTask(TaskModel task)
        {
            await _taskApi.DeleteAsync(task.Id);
            await _signalR.NotifyChange("delete", task);
            await LoadTasks();
        }

        private async Task LockTask(TaskModel task)
        {
            await _taskApi.LockAsync(task.Id);
            await _signalR.NotifyChange("lock", task);
            await LoadTasks();
        }

       

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}