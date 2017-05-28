using MicroMsg.sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BaoZouRiBao.Helper
{
    public class WeChatHelper
    {
        public const string APP_ID = "wx427ec9a62442b63f";

        #region Share to wechat's user
        /// <summary>
        /// 分享文本
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="description"></param>
        /// <param name="thumbnail"></param>
        public static async Task ShareTextAsync(string title, string text, string description, StorageFile thumbnail = null)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneChooseByUser;

                var message = new WXTextMessage
                {
                    Title = title,
                    Text = text,
                    Description = description,
                };

                if (thumbnail == null)
                {
                    thumbnail = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Images/ic_logo.png"));
                }

                if (thumbnail != null)
                {
                    using (var stream = await thumbnail.OpenReadAsync())
                    {
                        var pic = new byte[stream.Size];
                        await stream.AsStream().ReadAsync(pic, 0, pic.Length);
                        message.ThumbData = pic;
                    }
                }

                SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                await api.SendReq(req);
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 分享图片
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="thumbnail"></param>
        public static async Task ShareImageAsync(string title, string description, string imageUrl, StorageFile thumbnail = null)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneChooseByUser;
                 
                var message = new WXImageMessage
                {
                    Title = title,
                    Description = description,
                    ImageUrl = imageUrl
                };
                 
                if (thumbnail == null)
                {
                    thumbnail = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Images/ic_logo.png"));
                }

                if (thumbnail != null)
                {
                    using (var stream = await thumbnail.OpenReadAsync())
                    {
                        var pic = new byte[stream.Size];
                        await stream.AsStream().ReadAsync(pic, 0, pic.Length);
                        message.ThumbData = pic;
                    }
                }

                SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                await api.SendReq(req);
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 分享网页
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="url"></param>
        /// <param name="thumbnail"></param>
        /// <returns></returns>
        public static async Task ShareWebAsync(string title, string description, string url, StorageFile thumbnail = null)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneChooseByUser;

                if (thumbnail == null)
                {
                    thumbnail = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Images/ic_logo.png"));
                }

                var message = new WXWebpageMessage
                {
                    WebpageUrl = url,
                    Title = title,
                    Description = description
                };
                if (thumbnail != null)
                {
                    using (var stream = await thumbnail.OpenReadAsync())
                    {
                        byte[] pic = new byte[stream.Size];
                        await stream.AsStream().ReadAsync(pic, 0, pic.Length);
                        message.ThumbData = pic;
                    }
                }

                SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                await api.SendReq(req);
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 授权登录
        /// </summary>
        public static async void Login()
        {
            try
            {
                SendAuth.Req req = new SendAuth.Req("", "test");
                IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                var isValid = await api.SendReq(req);
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        
    }
}
