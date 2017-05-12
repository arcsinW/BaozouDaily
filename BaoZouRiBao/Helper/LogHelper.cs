using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    ///  日志
    /// </summary>
    internal class LogHelper
    {
        private static StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        private static ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();

        private static StorageFile logFile;

        private static DispatcherTimer timer = new DispatcherTimer();

        static LogHelper()
        {
            Initial();
            timer.Interval = new TimeSpan(0, 0, 4);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static async void Initial()
        {
            string filename = DateTime.Now.ToString("yyyy-MM-dd");
            logFile = await localFolder.CreateFileAsync($"{filename}.log", CreationCollisionOption.OpenIfExists);
        }

        private static async void Timer_Tick(object sender, object e)
        {
            while (logQueue.Count > 0)
            {
                string message = string.Empty;
                logQueue.TryDequeue(out message);
                await WriteLogToFile(message);
            }
            timer.Stop();
        }

        private static async Task WriteLogToFile(string message)
        {
            StringBuilder sb = new StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  "));
            sb.Append(message);
            sb.Append(Environment.NewLine);

            try
            {
                await FileIO.AppendTextAsync(logFile, sb.ToString());
            }
            catch (FileNotFoundException)
            {
                Initial();
            }
        }

        public static void WriteLine(string message, LogTypeEnum logType = LogTypeEnum.Info)
        {
            Debug.WriteLine(message);

            logQueue.Enqueue($"[{logType}]  {message}");

            if (!timer.IsEnabled)
            {
                timer.Start();
            }
        }
    
        public static void WriteLine(Exception e,[CallerMemberName]string name = "")
        {
            Debug.WriteLine($"[{name} {e.Message}");
        }
    }

    /// <summary>
    /// 日志级别
    /// </summary>
    internal enum LogTypeEnum
    {
        Info,
        Error
    }
}
