using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BaoZouRiBao.Converter
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        { 
            switch((string)value)
            {
                case "False":
                case "false":
                    return Application.Current.Resources["ThemeColorBrush"] as SolidColorBrush;
                case "True":
                case "true":
                    return new SolidColorBrush(Colors.Gray);
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
