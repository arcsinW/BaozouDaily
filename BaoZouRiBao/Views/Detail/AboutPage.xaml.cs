using BaoZouRiBao.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            historiesTextBlock.Text = await GetWordsAsync();
        }

        private async Task<string> GetWordsAsync()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Data/update_histories.txt"));
            string text = await FileIO.ReadTextAsync(file);
            return text;
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText("473967668");
            Clipboard.SetContent(dataPackage);
            ToastService.SendToast("已复制到剪切板");
        }
    }
}
