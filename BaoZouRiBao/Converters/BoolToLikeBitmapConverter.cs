using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace BaoZouRiBao.Converters
{
    /// <summary>
    /// bool -> like or dislike
    /// </summary>
    public class BoolToLikeBitmapConverter : BoolToObjectConverter
    {
        public BoolToLikeBitmapConverter()
        {
            TrueValue = new BitmapImage(new Uri("ms-appx:///Assets/Images/btn_article_like_pre.png"));
            FalseValue = new BitmapImage(new Uri("ms-appx:///Assets/Images/btn_article_like_nor.png"));
        }
    }
}
