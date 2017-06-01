using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace BaoZouRiBao.Converters
{
    public class BoolToVoteBitmapConverter : BoolToObjectConverter
    {
        public BoolToVoteBitmapConverter()
        {
            TrueValue = new Uri("ms-appx:///Assets/Images/btn_actionbar_like_pre.png");
            FalseValue = new Uri("ms-appx:///Assets/Images/btn_actionbar_like_nor.png");
        }
    }
}
