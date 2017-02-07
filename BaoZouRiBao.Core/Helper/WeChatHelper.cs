using MicroMsg.sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BaoZouRiBao.Core.Helper
{
    public class WeChatHelper
    {
        public const string APP_ID = "wx427ec9a62442b63f";

        public async void ShareText(string text)
        {
            try
            {
                var scene = SendMessageToWX.Req.WXSceneChooseByUser;
                var message = new WXTextMessage
                {
                    Title = "WeChat SDK Test",
                    Text = DateTimeHelper.GetUnixTimeStamp(),
                    Description = "description",
                    ThumbData = null
                };
                SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                IWXAPI api = WXAPIFactory.CreateWXAPI(APP_ID);
                var isValid = await api.SendReq(req);
            }
            catch(WXException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async void ShareImage(StorageFile file)
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
                        Title = "Picture Title",
                        Description = "Image ",
                        ThumbData = pic,
                        ImageUrl = ""
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

        public async void ShareWeb(StorageFile image)
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
                        Title = "Link",
                        Description = "Description",
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
    }
}
