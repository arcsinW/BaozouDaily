using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace BaoZouRiBao.Converter
{
    public class BoolToUriConverter : BoolToObjectConverter
    {
        public BoolToUriConverter()
        {
            TrueValue = new BitmapImage(new Uri("ms-appx:///Assets/Images/btn_nav_favourite_pre.png"));
            FalseValue = new BitmapImage(new Uri("ms-appx:///Assets/Images/sidebar_favourite.png"));
        }

        //public object Convert(object value, Type targetType, object parameter, string language)
        //{
        //    string favorited = (string)value;
        //    if (string.IsNullOrEmpty(favorited))
        //    {
        //        return new Uri("ms-appx:///Assets/Images/sidebar_favourite.png");
        //    }

        //    switch ((string)value)
        //    {
        //        case "True":
        //            return new Uri("ms-appx:///Assets/Images/nav_favourite_pre.png");
        //        case "False":
        //            return new Uri("ms-appx:///Assets/Images/sidebar_favourite.png");
        //    }

        //    return new Uri("ms-appx:///Assets/Images/sidebar_favourite.png");
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, string language)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
