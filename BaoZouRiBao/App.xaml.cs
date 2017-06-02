using BaoZouRiBao.Utils;
using BaoZouRiBao.Model;
using BaoZouRiBao.Views;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.QueryStringDotNET;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Data.Json;
using Windows.Media.SpeechRecognition;
using Windows.Networking.PushNotifications;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using BaoZouRiBao.Helper;

namespace BaoZouRiBao
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += App_UnhandledException;
            //HockeyClient.Current.Configure("ea627d35afcc4f81b8eb033f9b2e79df");
        }

        private void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            LogHelper.WriteLine(e.Exception);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            LogHelper.WriteLine("OnLaunched");

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;
                  
                if (e.PreviousExecutionState != ApplicationExecutionState.Running)
                {
                    //TODO: Load state from previously suspended application
                    bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                    ExtendedSplash extendedSplash = new ExtendedSplash(e.SplashScreen, loadState);
                    rootFrame.Content = extendedSplash;
                    Window.Current.Content = rootFrame;
                } 
            }
             
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MasterDetailPage), e.Arguments, new SlideNavigationTransitionInfo());
            }

            // Ensure the current window is active
            Window.Current.Activate();

            // 通过Toast通知从前台打开
            HandleNotification(e);

            PrepareExtraFunction();
        }
        

        /// <summary>
        /// 为应用增加额外功能
        /// </summary>
        private void PrepareExtraFunction()
        {
            //VoiceCommandHelper.InstallVCDFile();
            MobileCenter.Start(GlobalValue.MobileCenterKey, typeof(Analytics));
            //BackgroundTaskRegisterHelper.RegisterAll();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            LogHelper.WriteLine(e.Exception);
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            LogHelper.WriteLine("OnActivated");

            //base.OnActivated(args);

            // Repeat the same basic initialization as OnLaunched() above, taking into account whether
            // or not the app is already active.
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active.
            if (rootFrame == null)
            {
                // Create a frame to act as the navigation context and navigate to the first page.
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                // Place the frame in the current window.
                Window.Current.Content = rootFrame;
            }
             
            // Ensure the current window is active
            Window.Current.Activate();
             
            // Since we're expecting to always show a details page, navigate even if 
            // a content frame is in place (unlike OnLaunched).
            // Navigate to either the main trip list page, or if a valid voice command
            // was provided, to the details page for that trip.
            //rootFrame.Navigate(navigationToPageType, navigationCommand);
            HandleNotification(args);
        }

        private ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// 处理通知
        /// </summary>
        /// <param name="args"></param>
        private void HandleNotification(IActivatedEventArgs args)
        {
            // 通过Toast通知从后台打开
            if (args is ToastNotificationActivatedEventArgs)
            {
                var toastActivationArgs = args as ToastNotificationActivatedEventArgs;

                string argument = (string)LocalSettings.Values["args"];

                QueryString queryString;
                if (!string.IsNullOrEmpty(toastActivationArgs.Argument))
                {
                    queryString = QueryString.Parse(toastActivationArgs.Argument);
                }
                else
                {
                    queryString = QueryString.Parse(argument);
                }
                  
                string type = string.Empty;
                if (!queryString.TryGetValue("type", out type))
                {
                    return;
                }

                switch (type)
                {
                    // 首页
                    case "mainpage":
                    // 投稿
                    case "contribute":
                        WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = queryString["uri"], DocumentId = queryString["documentId"], DisplayType = queryString["displayType"] };

                        try
                        {
                            if (MasterDetailPage.Current == null)
                            {
                                Frame rootFrame = Window.Current.Content as Frame;
                                if (rootFrame.Content == null)
                                {
                                    // When the navigation stack isn't restored navigate to the first page,
                                    // configuring the new page by passing required information as a navigation
                                    // parameter
                                    rootFrame.Navigate(typeof(MasterDetailPage), new SlideNavigationTransitionInfo());
                                }
                            }

                            NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), parameter);
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLine(e);
                        }
                        break;
                    // 视频
                    case "video":
                        NavigationHelper.DetailFrameNavigate(typeof(VideoPage), queryString["documentId"]);
                        break;
                }
            }
        }

        /// <summary>
        /// 处理语音命令
        /// </summary>
        /// <param name="args"></param>
        private void HandleVoiceCommand(IActivatedEventArgs args)
        {
            Type navigationToPageType;

            if (args.Kind == ActivationKind.VoiceCommand)
            {
                var cmdArgs = args as VoiceCommandActivatedEventArgs;

                var speechRecognitionResult = cmdArgs.Result;

                string voiceCommandName = speechRecognitionResult.RulePath[0];
                string text = speechRecognitionResult.Text;

                //speech or text
                string commandMode = this.SemanticInterpretation("commandMode", speechRecognitionResult);

                switch (voiceCommandName)
                {
                    case "search":
                        string keyword = this.SemanticInterpretation("keyword", speechRecognitionResult);
                        navigationToPageType = typeof(SearchPage);
                        break;
                }
            }
            // Protocol activation occurs when a card is clicked within Cortana (using a background task).
            else if (args.Kind == ActivationKind.Protocol)
            {
                // Extract the launch context. In this case, we're just using the destination from the phrase set (passed
                // along in the background task inside Cortana), which makes no attempt to be unique. A unique id or 
                // identifier is ideal for more complex scenarios. We let the destination page check if the 
                // destination trip still exists, and navigate back to the trip list if it doesn't.
                var commandArgs = args as ProtocolActivatedEventArgs;
                Windows.Foundation.WwwFormUrlDecoder decoder = new Windows.Foundation.WwwFormUrlDecoder(commandArgs.Uri.Query);
                var destination = decoder.GetFirstValueByName("LaunchContext");
            }
            else
            {
                // If we were launched via any other mechanism, fall back to the main page view.
                // Otherwise, we'll hang at a splash screen.

            }
        }

        /// <summary>
        /// Returns the semantic interpretation of a speech result. 
        /// Returns null if there is no interpretation for that key.
        /// </summary>
        /// <param name="interpretationKey">The interpretation key.</param>
        /// <param name="speechRecognitionResult">The speech recognition result to get the semantic interpretation from.</param>
        /// <returns></returns>
        private string SemanticInterpretation(string interpretationKey, SpeechRecognitionResult speechRecognitionResult)
        {
            return speechRecognitionResult.SemanticInterpretation.Properties[interpretationKey].FirstOrDefault();
        }
    }
}
