using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WpfTodoApp.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        private int _id;
        private string _description;
        private string _title;
        private int _priority;
        private bool _isCompleted;
        private DateTime _createdAt;
        private DateTime? _dueDate;
        private DateTime _lastModified;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }


        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public int Priority
        {
            get => _priority;
            set { _priority = value; OnPropertyChanged(); }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set { _isCompleted = value; OnPropertyChanged(); }
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set { _createdAt = value; OnPropertyChanged(); }
        }

        public DateTime? DueDate
        {
            get => _dueDate;
            set { _dueDate = value; OnPropertyChanged(); }
        }

        public DateTime LastModified
        {
            get => _lastModified;
            set { _lastModified = value; OnPropertyChanged(); }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value; OnPropertyChanged();
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}