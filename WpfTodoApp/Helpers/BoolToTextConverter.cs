using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfTodoApp
{
    public class BoolToTextConverter : IValueConverter
    {
        public string TrueText { get; set; } = "Cancelar";
        public string FalseText { get; set; } = "Nueva tarea";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value ? TrueText : FalseText;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Binding.DoNothing;
    }
}
