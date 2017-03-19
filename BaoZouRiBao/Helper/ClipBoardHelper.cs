using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace BaoZouRiBao.Helper
{
    public class ClipBoardHelper
    {
        static ClipBoardHelper()
        {
            Clipboard.ContentChanged += Clipboard_ContentChanged;
        }

        private static async void Clipboard_ContentChanged(object sender, object e)
        {
            DataPackageView dataPackageView = Clipboard.GetContent();
            string text = await dataPackageView.GetTextAsync();
        }
    }
}
