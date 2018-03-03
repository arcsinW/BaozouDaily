using BaoZouRiBao.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

namespace BaoZouRiBao.Helper
{
    public class ShareHelper
    {
        /// <summary>
        /// 复制链接
        /// </summary>
        public static void CopyLink(string content)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(content);
            Clipboard.SetContent(dataPackage);

            ToastService.SendToast("链接已复制到剪切板");
        }

        /// <summary>
        /// 微信分享
        /// </summary>
        public static async void WeChatShare(string title, string shareUrl)
        {
            await WeChatHelper.ShareWebAsync(title, "", shareUrl);
        }

        /// <summary>
        /// 系统分享
        /// </summary>
        /// <param name="title"></param>
        /// <param name="shareUrl"></param>
        public static void SystemShare(string title, string shareUrl)
        {
            DataTransferManager.GetForCurrentView().DataRequested += (sender, args) =>
            {
                var deferral = args.Request.GetDeferral();

                args.Request.Data.Properties.Title = title;
                args.Request.Data.SetWebLink(new Uri(shareUrl));
                 
                deferral.Complete();
            };

            DataTransferManager.ShowShareUI();
        }

        /// <summary>
        /// 系统分享图片
        /// </summary>
        /// <param name="title"></param>
        /// <param name="shareUrl"></param>
        public static void SystemShare(string title, StorageFile thumbnail = null)
        {
            DataTransferManager.GetForCurrentView().DataRequested += (sender, args) =>
            {
                var deferral = args.Request.GetDeferral();

                args.Request.Data.Properties.Title = title;

                List<IStorageItem> imgs = new List<IStorageItem>() { thumbnail };
                args.Request.Data.SetStorageItems(imgs);

                RandomAccessStreamReference imgRef = RandomAccessStreamReference.CreateFromFile(thumbnail);
                args.Request.Data.Properties.Thumbnail = imgRef;
                args.Request.Data.SetBitmap(imgRef);

                deferral.Complete();
            };

            DataTransferManager.ShowShareUI();
        }
    }
}
