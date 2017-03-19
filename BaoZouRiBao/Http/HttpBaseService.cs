using BaoZouRiBao.Helper;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace BaoZouRiBao.Http
{
    /// <summary>
    /// provide basic http function
    /// </summary>
    public class HttpBaseService
    {
        private static HttpClient httpClient = new HttpClient();

        static HttpBaseService()
        {
            var header = httpClient.DefaultRequestHeaders;
            //！document will not got without this header
            header.UserAgent.TryParseAdd("Mozilla/5.0 (Windows Phone 10.0; Android 4.2.1; NOKIA; Lumia 830) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Mobile Safari/537.36 Edge/14.14295");

            header.Accept.TryParseAdd("text/html, application/xhtml+xml, image/jxr, */*");
            header.AcceptEncoding.TryParseAdd("gzip,deflate");
            header.AcceptLanguage.TryParseAdd("en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3");
            //header["X-APP-VERSION_CODE"] = "7";
            //header["X-APP-VERSION"] = "3.1.0";
            //header["ZA"] = "OS=Android 5.0&Platform=4.5 Lollipop (5.0) XHDPI Phone";
            //header["Timestamp"] = Functions.GetUnixTimeStamp();
            //header.Host = new Windows.Networking.HostName("dailyapi.ibaozou.com");
            header.Connection.TryParseAdd("Keep-Alive");
            header["Host"] = "dailyapi.ibaozou.com";
            //header["Sign"] = "04be2eaa0cb7f683fff807fd09110aeb";
        }

        /// <summary>
        /// 向服务器发送get请求  返回服务器回复数据(string)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> SendGetRequest(string uri)
        {
            try
            {
                if (!string.IsNullOrEmpty(GlobalValue.Current.AccessToken))
                {
                    var header = httpClient.DefaultRequestHeaders;
                    header["Authorization"] = "Bearer " + GlobalValue.Current.AccessToken;
                }
                string res = await httpClient.GetStringAsync(new Uri(uri)); 
                return res;
            }
            catch(Exception e)
            {
                Debug.WriteLine("HttpBaseService SendGetRequest:" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 向服务器发送post请求 返回服务器回复数据(string)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<string> SendPostRequest(string uri, string body)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
                request.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json; charset=utf-8");
                HttpResponseMessage response = await httpClient.SendRequestAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                Debug.Write(e.Message);
                return null;
            }
        }

        //public async static Task<string> SendBytesPostRequest(string uri ,byte[] data)
        //{
        //    try
        //    {
        //        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
         

        //    }
        //    catch(Exception e)
        //    {

        //    }
        //}

        /// <summary>
        /// 向服务器发送get请求  返回服务器回复数据(bytes)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<IBuffer> SendGetRequestAsBytes(string uri)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(uri));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsBufferAsync();
            }
            catch(Exception e)
            {
                Debug.WriteLine("HttpBaseService SendGetRequestAsBytes:" + e.Message);
                return null;
            }
        }

        public async static Task<string> SendDicPostRequest(Dictionary<string, string> dic, string uri) 
        {
            try
            { 
                HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(dic);
                HttpResponseMessage msg = await httpClient.PostAsync(new Uri(uri), content);
                return msg.Content.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SendRequest : " + ex.Message);
                return null;
            }
        }


        public static void AddHeader(string key, string value)
        {
            var header = httpClient.DefaultRequestHeaders;
            
            httpClient.DefaultRequestHeaders[key] = value;
        }
    }
}
