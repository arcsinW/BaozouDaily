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
        public static async void ShowStatusBar()
        {
            
            if (DeviceInformationHelper.IsMobile())
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundOpacity = 1;
                statusBar.BackgroundColor = App.Current.Resources["StatusBarBackgroundColor"] as Color?;
                statusBar.ForegroundColor = Colors.White;
                await statusBar.ShowAsync();
            }
            else
            { 
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = App.Current.Resources["LightThemeColor"] as Color?;
                titleBar.ButtonBackgroundColor = App.Current.Resources["LightThemeColor"] as Color?;
                titleBar.ForegroundColor = Colors.White;
            }
        }
    }
}
