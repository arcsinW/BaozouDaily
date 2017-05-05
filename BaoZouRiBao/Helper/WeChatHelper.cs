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
        /// 文本分享给微信好友
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="description"></param>
        /// <param name="thumb"></param>
        public static async void ShareText(string title, string text, string description, byte[] thumb)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneChooseByUser;
                var message = new WXTextMessage
                {
                    Title = title,
                    Text = text,
                    Description = description,
                    ThumbData = thumb
                };
                SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                var isValid = await api.SendReq(req);
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 图片分享给微信好友
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="file"></param>
        public static async void ShareImage(string title, string description, string imageUrl, StorageFile file)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneChooseByUser;
                using (var stream = await file.OpenReadAsync())
                {
                    var pic = new byte[stream.Size];
                    await stream.AsStream().ReadAsync(pic, 0, pic.Length);
                    var message = new WXImageMessage
                    {
                        Title = title,
                        Description = description,
                        ThumbData = pic,
                        ImageUrl = imageUrl
                    };
                    SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                    IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                    var isValid = await api.SendReq(req);
                }
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 网页分享给微信好友
        /// </summary>
        /// <param name="image"></param>
        public static async void ShareWeb(string title, string description, string url, StorageFile image)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneChooseByUser;
                using (var stream = await image.OpenReadAsync())
                {
                    byte[] pic = new byte[stream.Size];
                    await stream.AsStream().ReadAsync(pic, 0, pic.Length);
                    var message = new WXWebpageMessage
                    {
                        WebpageUrl = "http://www.baidu.com",
                        Title = title,
                        Description = description,
                        ThumbData = pic
                    };
                    SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                    IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                    var isValid = await api.SendReq(req);
                }
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Share to wechat's timeline
        /// <summary>
        /// 文本分享给微信朋友圈
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="description"></param>
        /// <param name="thumb"></param>
        public static async void ShareTextToTimeLine(string title, string text, string description, byte[] thumb)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneTimeline;
                var message = new WXTextMessage
                {
                    Title = title,
                    Text = text,
                    Description = description,
                    ThumbData = thumb
                };
                SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                var isValid = await api.SendReq(req);
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 图片分享给微信朋友圈
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="file"></param>
        public static async void ShareImageToTimeLine(string title, string description, string imageUrl, StorageFile file)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneTimeline;
                using (var stream = await file.OpenReadAsync())
                {
                    var pic = new byte[stream.Size];
                    await stream.AsStream().ReadAsync(pic, 0, pic.Length);
                    var message = new WXImageMessage
                    {
                        Title = title,
                        Description = description,
                        ThumbData = pic,
                        ImageUrl = imageUrl
                    };
                    SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                    IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                    var isValid = await api.SendReq(req);
                }
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 网页分享给微信朋友圈
        /// </summary>
        /// <param name="image"></param>
        public static async void ShareWebToTimeLine(string title, string description, string url, StorageFile image)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneTimeline;
                using (var stream = await image.OpenReadAsync())
                {
                    byte[] pic = new byte[stream.Size];
                    await stream.AsStream().ReadAsync(pic, 0, pic.Length);
                    var message = new WXWebpageMessage
                    {
                        WebpageUrl = url,
                        Title = title,
                        Description = description,
                        ThumbData = pic
                    };
                    SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                    IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                    var isValid = await api.SendReq(req);
                }
            }
            catch (WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        } 
        #endregion
    }
}
