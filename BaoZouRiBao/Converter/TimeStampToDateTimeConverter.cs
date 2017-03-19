using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.Converter
{
    public class TimeStampToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            long timeStamp =  (long)value;

            return DateTimeHelper.UnixTimeStampToDateTime(timeStamp).ToString("yyyy-MM-dd HH:MM:ss");
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
