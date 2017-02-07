using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BaoZouRiBao.Converter
{
    /// <summary>
    /// This converter helps mapping the selection state of a list legend item
    /// to a color value.
    /// </summary>
    public class SelectedLegendItemToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isSelected = (bool)value;

            return isSelected
                ? Application.Current.Resources["ThemeColorBrush"] as Brush
                : Application.Current.Resources["AdjustColorBrush"] as Brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
