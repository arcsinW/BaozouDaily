using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
 
namespace BaoZouRiBao.UserControls
{
    public class DialogService : ContentControl
    {
        Grid _rootGrid;
        ContentPresenter _content;
        Border _maskerBorder;

        Storyboard _showStory, _hideStory;

        public object OriginalSource { get; set; }

        public string OriginalSourceType { get; set; }

        public bool IsHide => _content == null ? true : (_content.Visibility == Visibility.Collapsed ? true : false);

        public DialogService()
        {
            this.DefaultStyleKey = typeof(DialogService);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (!DesignMode.DesignModeEnabled)
            {
                InitializeDialog();
            }
        }



        private async void MaskerBorder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await HideAsync();
        }



        public void Show()
        {
            _content.Visibility = Visibility.Visible;
            _maskerBorder.Visibility = Visibility.Visible;
            _showStory.Begin();
        }

        public async Task HideAsync()
        {
            _hideStory.Begin();

            await Task.Delay(300);
            _content.Visibility = Visibility.Collapsed;
            _maskerBorder.Visibility = Visibility.Collapsed;
        }

        private void InitializeDialog()
        {
            _rootGrid = GetTemplateChild("RootGrid") as Grid;
            _content = GetTemplateChild("DialogContent") as ContentPresenter;
            _maskerBorder = GetTemplateChild("MaskerBorder") as Border;

            _showStory = _rootGrid.Resources["ShowStory"] as Storyboard;
            _hideStory = _rootGrid.Resources["HideStory"] as Storyboard;

            _maskerBorder.Tapped += MaskerBorder_Tapped;

            _content.Visibility = Visibility.Collapsed;
            _maskerBorder.Visibility = Visibility.Collapsed;
        }

    }
}
