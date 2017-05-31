using BaoZouRiBao.Controls;
using BaoZouRiBao.Enums;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.UserControls
{
    public sealed partial class TaskDialog : DialogService
    {
        public TaskDialog()
        {
            this.InitializeComponent();
        }

        public TaskDialog(DailyTaskDoneResult result)
        {
            BaoZouPopupType = result.Task.TaskType;
            CoinCount = result.Task.Amount;
            Title = result.AlertDesc;
            this.InitializeComponent();
        }

        public void Show(DailyTaskDoneResult result)
        {
            BaoZouPopupType = result.Task.TaskType;
            CoinCount = result.Task.Amount;
            Title = result.AlertDesc;
        }
         
        #region Dependency Properties
        public BaoZouTaskEnum BaoZouPopupType
        {
            get { return (BaoZouTaskEnum)GetValue(PopupTypeProperty); }
            set { SetValue(PopupTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupTypeProperty =
            DependencyProperty.Register("BaoZouPopupType", typeof(BaoZouTaskEnum), typeof(TaskDialog), new PropertyMetadata(0,BaoZouPopupTypeChangedCallback));

        private static void BaoZouPopupTypeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TaskDialog dialog = d as TaskDialog;
            if (dialog != null)
            {
                dialog.UpdateUriSource(dialog.BaoZouPopupType);
            }
        }

        private void UpdateUriSource(BaoZouTaskEnum task)
        {
            UriSource = new Uri("ms-appx:///Assets/Images/img_popout_box_" + task.ToString() + ".png");
        }

        public string CoinCount
        {
            get { return (string)GetValue(CoinCountProperty); }
            set { SetValue(CoinCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CoinCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoinCountProperty =
            DependencyProperty.Register("CoinCount", typeof(string), typeof(TaskDialog), new PropertyMetadata(0));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(TaskDialog), new PropertyMetadata(0));

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(TaskDialog), new PropertyMetadata(0));

        public Uri UriSource
        {
            get { return (Uri)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UriSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriSourceProperty =
            DependencyProperty.Register("UriSource", typeof(Uri), typeof(TaskDialog), new PropertyMetadata(0, propertyChangedCallback));

        private static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TaskDialog dialog = d as TaskDialog;
            if (dialog != null)
            {
                dialog.image.Source = new BitmapImage() { UriSource = (Uri)e.NewValue };
            }
        }
        #endregion
    }
}
