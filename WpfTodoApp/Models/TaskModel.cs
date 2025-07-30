using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfTodoApp.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool IsCompleted { get; set; }

        // INotifyPropertyChanged para actualizar la UI automáticamente
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
