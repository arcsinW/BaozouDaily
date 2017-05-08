using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace BaoZouRiBao.Converters
{
    public class ParentToTextBlockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            CommentParent p = (CommentParent)value;
            if (p != null)
            { 
                Run name = new Run();
                name.Foreground = App.Current.Resources["ThemeColorBrush"] as SolidColorBrush;
                name.Text = $"@{p.UserName}";

                return name;
            }
            return new Run();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
