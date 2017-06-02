using BaoZouRiBao.EventHandlers;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace BaoZouRiBao.Http
{
    /// <summary>
    /// Provide basic http function and fire UnAuthorized event
    /// </summary>
    public class HttpBaseService
    {
        public static event UnauthorizedEventHandler OnUnAuthorized;

        private static HttpClient httpClient = new HttpClient();

        static HttpBaseService()
        {
            var header = httpClient.DefaultRequestHeaders;

            // ！document will not got without this header
            header.UserAgent.TryParseAdd("Mozilla/5.0 (Windows Phone 10.0; Android 4.2.1; NOKIA; Lumia 830) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Mobile Safari/537.36 Edge/14.14295");
            header.Accept.TryParseAdd("text/html, application/xhtml+xml, image/jxr, */*");
            header.AcceptEncoding.TryParseAdd("gzip,deflate");
            header.AcceptLanguage.TryParseAdd("en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3");

            // header["X-APP-VERSION_CODE"] = "7";
            // header["X-APP-VERSION"] = "3.1.0";
            // header["ZA"] = "OS=Android 5.0&Platform=4.5 Lollipop (5.0) XHDPI Phone";
            header["Timestamp"] = DateTimeHelper.GetUnixTimeStamp();
            
            header.Connection.TryParseAdd("Keep-Alive");
            header["Host"] = "dailyapi.ibaozou.com";

            OnUnAuthorized += HttpBaseService_OnUnAuthorized;
        }

        ~HttpBaseService()
        {
            OnUnAuthorized -= HttpBaseService_OnUnAuthorized;
        }

        private static void HttpBaseService_OnUnAuthorized()
        {
            NavigationHelper.DetailFrameNavigate(typeof(BaozouLoginPage));
        }

        //if (!string.IsNullOrEmpty(GlobalValue.Current.User.AccessToken))
        //{
        //    var header = httpClient.DefaultRequestHeaders;
        //    header["Authorization"] = "Bearer " + GlobalValue.Current.User.AccessToken;
        //}

        /// <summary>
        /// 向服务器发送get请求  返回服务器回复数据(string)
        /// </summary>
        /// <param name="uri">API地址</param>
        /// <returns></returns>
        public static async Task<string> SendGetRequest(string uri)
        {
            try
            {   
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(uri));
                if (response.StatusCode == Windows.Web.Http.HttpStatusCode.Unauthorized)
                {
                    OnUnAuthorized?.Invoke();
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                LogHelper.WriteLine($"HttpBaseService.SendGetRequest : {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 向服务器发送post请求 返回服务器回复数据(string)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static async Task<string> SendPostRequest(string uri, string body)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
                request.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json; charset=utf-8");
                HttpResponseMessage response = await httpClient.SendRequestAsync(request);

                if (response.StatusCode == Windows.Web.Http.HttpStatusCode.Unauthorized)
                {
                    OnUnAuthorized?.Invoke();
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.Write($"HttpBaseService.SendPostRequest {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 向服务器发送get请求  返回服务器回复数据(bytes)
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<IBuffer> SendGetRequestAsBytes(string uri)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(uri));

                if (response.StatusCode == Windows.Web.Http.HttpStatusCode.Unauthorized)
                {
                    OnUnAuthorized?.Invoke();
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsBufferAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"HttpBaseService.SendGetRequestAsBytes {e.Message}");
                return null;
            }
        }

        public static async Task<string> SendDicPostRequest(Dictionary<string, string> dic, string uri) 
        {
            try
            {
                HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(dic);
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(uri), content);

                if (response.StatusCode == Windows.Web.Http.HttpStatusCode.Unauthorized)
                {
                    OnUnAuthorized?.Invoke();
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"HttpBaseService.SendDicPostRequest : {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 修改Http Header
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetHeader(string key, string value)
        {
            var header = httpClient.DefaultRequestHeaders;

            httpClient.DefaultRequestHeaders[key] = value;
        }

        /// <summary>
        /// 移除一个Http Header
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveHeader(string key)
        {
            if (httpClient.DefaultRequestHeaders.ContainsKey(key))
            {
                httpClient.DefaultRequestHeaders.Remove(key);
            }
        }
    }
}
