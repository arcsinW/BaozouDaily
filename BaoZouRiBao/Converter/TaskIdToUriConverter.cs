using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.Converter
{
    public class TaskIdToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((string)value)
            {
                case "1":
                    return new Uri("ms-appx:///Assets/Images/self_sign.png");
                  
                case "2":
                    return new Uri("ms-appx:///Assets/Images/self_article.png");
                    
                case "3":
                    return new Uri("ms-appx:///Assets/Images/self_share.png");
                
                case "4":
                    return new Uri("ms-appx:///Assets/Images/self_article_like.png");
                 
                case "5":
                    return new Uri("ms-appx:///Assets/Images/self_comment_like.png");
                  
                case "6":
                    return new Uri("ms-appx:///Assets/Images/self_comment.png");
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
