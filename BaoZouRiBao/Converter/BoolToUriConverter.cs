using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.Converter
{
    public class BoolToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string favorited = (string)value;
            if(string.IsNullOrEmpty(favorited))
            {
                return new Uri("ms-appx:///Assets/Images/sidebar_favourite.png");
            }
            switch ((string)value)
            {
                case "True":
                    return new Uri("ms-appx:///Assets/Images/nav_favourite_pre.png");
                case "False":
                    return new Uri("ms-appx:///Assets/Images/sidebar_favourite.png");
            }
            return new Uri("ms-appx:///Assets/Images/sidebar_favourite.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
