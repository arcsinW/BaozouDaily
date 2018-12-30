using BaoZouRiBao.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace BaoZouRiBao.Helper
{
    public class UrlSignHelper
    {
        private static string SECRET_KEY = "18a75cf12dff8cf6e17550e25c860839";

        public static string MD5EncryptString(string rawData)
        {
            var alogrithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(rawData, BinaryStringEncoding.Utf8);
            var hashed = alogrithm.HashData(buffer);
            var result = CryptographicBuffer.EncodeToHexString(hashed);
            return result;
        }

        public static byte[] MD5Encrypt(string rawData)
        {
            var alogrithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(rawData, BinaryStringEncoding.Utf8);
            var hashed = alogrithm.HashData(buffer);
            CryptographicBuffer.CopyToByteArray(hashed, out byte[] result);
            return result;
        }

        public static String Get32MD5(String paramString)
        {
            int j = 0;
            
            char[] arrayOfChar1 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            try
            {
                byte[] arrayOfByte1 = Encoding.UTF8.GetBytes(paramString);
                byte[] arrayOfByte2 = MD5Encrypt(paramString);
                 
                int k = arrayOfByte2.Length;
                char[] arrayOfChar2 = new char[k * 2];
                int m = 0;
                while (j < k)
                {
                    int n = arrayOfByte2[j];
                    int i1 = m + 1;
                    arrayOfChar2[m] = arrayOfChar1[(0xF & n >> 4)];
                    m = i1 + 1;
                    arrayOfChar2[i1] = arrayOfChar1[(n & 0xF)];
                    j++;
                }
                String str = new String(arrayOfChar2);
                return str;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

            return "";
        }

        public static String Get16MD5(String paramString)
        {
            try
            {
                byte[] arrayOfByte = MD5Encrypt(paramString);
                StringBuilder localStringBuffer = new StringBuilder("");
                for (int j = 0; j < arrayOfByte.Length; j++)
                {
                    int k = arrayOfByte[j];
                    if (k < 0)
                    {
                        k += 256;
                    }

                    if (k < 16)
                    {
                        localStringBuffer.Append("0");
                    }

                    localStringBuffer.Append(Convert.ToString(k, 16));
                }

                String str = localStringBuffer.ToString().Substring(8, 24);
                return str;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static String GetMD5Sign(Dictionary<String, String> paraMap, List<String> paramList)
        {
            StringBuilder localStringBuilder = new StringBuilder();
            if ((paraMap == null) || (paramList == null))
            {
                return "";
            }

            paramList.Sort();
            for (int j = 0; j < paramList.Count; j++)
            {
                if (!paraMap.ContainsKey(paramList[j]))
                {
                    continue;
                }

                localStringBuilder.Append((String)paramList[j] + "=" + (String)paraMap[paramList[j]]);
            }

            localStringBuilder.Append(SECRET_KEY);
            String str1 = localStringBuilder.ToString();

            Debug.Write("Common", "sign = " + str1);

            try
            {
                string str2 = WebUtility.UrlEncode(str1);
                return Get32MD5(str2);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

            return "";
        }

        public static String GetSignedUrlBySigns(Dictionary<String, String> paramMap, List<String> paramList, String paramString)
        {
            String str1;
            String str2;
            StringBuilder localStringBuilder;
            try
            {
                str1 = paramString + "?";
                if ((paramMap == null) || (paramList == null))
                {
                    return "";
                }

                str2 = GetMD5Sign(paramMap, paramList);
                localStringBuilder = new StringBuilder();

                foreach (var item in paramMap)
                {

                    localStringBuilder.Append("&" + item.Key + "=" + item.Value);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }

            if (localStringBuilder.Length > 1)
            {
                localStringBuilder.Remove(0, 1);
                localStringBuilder.Append("&sign=" + str2);
            }

            localStringBuilder.Insert(0, str1);
            Debug.WriteLine("Common_getUrl", "" + localStringBuilder.ToString());
            String str3 = localStringBuilder.ToString();
            return str3;
        }

        public static String GetSignedUrlByExcpetSigns(Dictionary<String, String> paramDic, List<String> paramList, String paramString)
        {
            List<string> localparamlist = new List<string>();
            string url = string.Empty;
            try
            {
                foreach (var item in paramDic)
                {
                    localparamlist.Add(item.Key);
                }
                
                if (paramList == null)
                { 
                    url = GetSignedUrlBySigns(paramDic, localparamlist, paramString);
                    return url;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex);
                return "";
            }

            localparamlist.Except(paramList); 
            url = GetSignedUrlBySigns(paramDic, localparamlist, paramString);
            return url;
        }
    }
}
