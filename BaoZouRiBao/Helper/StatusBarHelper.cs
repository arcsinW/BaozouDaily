using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace BaoZouRiBao.Helper
{
    public class StatusBarHelper
    {
        public static async void ShowStatusBar(bool isNight)
        {
            if (InformationHelper.IsMobile)
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundOpacity = 1;
                statusBar.BackgroundColor = (isNight ? App.Current. Resources["DarkThemeColor"] : App.Current.Resources["LightThemeColor"]) as Color?;
                statusBar.ForegroundColor = Colors.White;
                await statusBar.ShowAsync();
            }
            else
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = (isNight ? App.Current.Resources["DarkThemeColor"] : App.Current.Resources["LightThemeColor"]) as Color?;
                titleBar.ButtonBackgroundColor = (isNight ? App.Current.Resources["DarkThemeColor"] : App.Current.Resources["LightThemeColor"]) as Color?;
                titleBar.ForegroundColor = Colors.White;
            }
        }
    }
}
