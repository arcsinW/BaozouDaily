using BaoZouRiBao.Core.Helper;
using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace BaoZouRiBao.Core.Http
{
    public class ApiBaseService
    {
        protected async Task<T> GetJson<T>(string url) where T : class
        {
            try
            {
                string json = await HttpBaseService.SendGetRequest(url);
                if (json != null)
                {
                    return JsonHelper.Deserlialize<T>(json);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }

        protected async Task<T> PostJson<T> (string uri,string body) where T : class
        {
            try
            {
                string json = await HttpBaseService.SendPostRequest(uri, body);
                if(json!= null)
                {
                    return JsonHelper.Deserlialize<T>(json);
                }
                return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        protected async Task<JsonObject>PostJson<T> (string uri,T t) where T : class
        {
            string body = JsonHelper.Serializer(t);
            try
            {
                string json = await HttpBaseService.SendPostRequest(uri, body);
                if (json != null)
                {
                    return JsonObject.Parse(json);
                }
                return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }
         
        protected async Task<ReturnT> PostJson<SendT,ReturnT>(string uri,SendT sendT) where ReturnT : class
        {
            string body = JsonHelper.Serializer(sendT);
            try
            {
                string json = await HttpBaseService.SendPostRequest(uri, body);
                if (json != null)
                {
                    return JsonHelper.Deserlialize<ReturnT>(json);
                }
                return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }
        
        protected async Task<ReturnT> PostDic<ReturnT>(Dictionary<string,string>dic ,string uri) where ReturnT : class
        {
            try
            {
                string result = await HttpBaseService.SendDicPostRequest(dic, uri);
                return JsonHelper.Deserlialize<ReturnT>(result);
            }
            catch(Exception e)
            {
                LogHelper.WriteLine(e);
                return null; 
            }
        }

        protected async Task<string> GetHtml(string url)
        {
            try
            {
                string html = await HttpBaseService.SendGetRequest(url);
                //byte[] bytes = Encoding.UTF8.GetBytes(html);
                //html = Encoding.GetEncoding("GBK").GetString(bytes);
                return html;
            }
            catch(Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }
        protected async Task<WriteableBitmap> GetImage(string url)
        {
            try
            {
                IBuffer buffer = await HttpBaseService.SendGetRequestAsBytes(url);
                if (buffer != null)
                {
                    BitmapImage bi = new BitmapImage();
                    WriteableBitmap wb = null;
                    using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                    {

                        Stream stream2Write = stream.AsStreamForWrite();

                        await stream2Write.WriteAsync(buffer.ToArray(), 0, (int)buffer.Length);

                        await stream2Write.FlushAsync();
                        stream.Seek(0);

                        await bi.SetSourceAsync(stream);

                        wb = new WriteableBitmap(bi.PixelWidth, bi.PixelHeight);
                        stream.Seek(0);
                        await wb.SetSourceAsync(stream);

                        return wb;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }

        protected async Task<AuthenticationResult> PostDicForLogin(string uri,Dictionary<string,string> dic) 
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var header = httpClient.DefaultRequestHeaders;
                header.AcceptEncoding.TryParseAdd("gzip");
                header.UserAgent.TryParseAdd("(Linux; Android 5.0; 4.5 Lollipop (5.0) XHDPI Phone Build/Android/VS Emulator 4.5 Lollipop (5.0) XHDPI Phone/donatello/LRX21L/en_US; baozouribao_android_app; 3.1.0; 27; miui)");

                HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(dic);
                HttpResponseMessage msg = await httpClient.PostAsync(new Uri(ServiceUri.OAuth2), content);
                return JsonHelper.Deserlialize<AuthenticationResult>(msg.Content.ToString());
            }
            catch (Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }
    }
}
