using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BaoZouRiBao.Helper;
using System.Threading.Tasks;
using BaoZouRiBao.Utils;
using System.Diagnostics;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.Storage;

namespace BaoZouRiBao.Views
{
    public sealed partial class SplashScreenPage : Page
    {
        public SplashScreenPage()
        {
            this.InitializeComponent();
        }

        public SplashScreenPage(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();

            AppTheme = DataShareManager.Current.AppTheme; 
             
            //StatusBarHelper.ShowStatusBar(AppTheme == ElementTheme.Dark);

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This ensures that the extended splash screen formats properly in response to window resizing.
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            ScaleFactor = Windows.Graphics.Display.DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;

            splash = splashscreen;
            if (splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;

                PositionElement();
            }

            // Create a Frame to act as the navigation context
            rootFrame = new Frame();
        }

        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;
        private double ScaleFactor;  ////Variable to hold the device scale factor (use to determine phone screen resolution)
        ElementTheme AppTheme { get; set; }

        #region 重定位控件

        void PositionElement()
        {
            PositionImage();
            PositionRing();
            PositionTextBlock();
        }
         
        void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                extendedSplashImage.Height = splashImageRect.Height / ScaleFactor;
                extendedSplashImage.Width = splashImageRect.Width / ScaleFactor;
            }
            else
            {
                extendedSplashImage.Height = splashImageRect.Height;
                extendedSplashImage.Width = splashImageRect.Width;
            }
        }

        void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
            splashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height * 1.1));

            splashProgressRing.Height = 36;
            splashProgressRing.Width = 36;

            splashProgressRing.IsActive = true;
        }

        void PositionTextBlock()
        {
            tipTextBlock.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (tipTextBlock.Width * 0.5));
            tipTextBlock.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height * 1));
        }
        #endregion

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        private async void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;
            
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await InitialSDKAsync();

                var rootFrame = (Window.Current.Content as Frame) as Frame;
                rootFrame?.Navigate(typeof(MasterDetailPage));
            });
        }
        
        void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            if (splash != null)
            {
                splashImageRect = splash.ImageLocation;

                PositionElement();
            }
        }

        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <returns></returns>
        private async Task InitialSDKAsync()
        {
            try
            {
                Lary.Apps.SDK.UniversalServices.Config.Initialize(GlobalValue.AccessKey);
                PushNotificationChannel pushNotificationChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                await Lary.Apps.SDK.UniversalServices.PushNotificationService.UploadChannelAsync(pushNotificationChannel.Uri, pushNotificationChannel.ExpirationTime);
                VoiceCommandHelper.InstallVCDFile();
                BackgroundTaskRegisterHelper.RegisterAll();
            }
            catch (Exception e)
            {
                tipTextBlock.Text = "穿山甲拒绝了本次采访";
                LogHelper.WriteLine(e);
            }
        }

        private async Task<string> GetWordsAsync()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Data/Text.txt"));
            var lines = await FileIO.ReadLinesAsync(file);
            int index = new Random().Next(0, lines.Count);
            return lines[index];
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tipTextBlock.Text = await GetWordsAsync();
        }
    }
}
