using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.Converter
{
    /// <summary>
    /// Converte seconds to MM:ss
    /// </summary>
    public class SecondsToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int seconds = int.Parse((string)value);
            return string.Format("{0:00}:{1:00}", seconds / 60, seconds % 60);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}