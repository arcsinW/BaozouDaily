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
    public class MD5Helper
    {
        //    public static string EncryptString(string data)
        //    {
        //        HashAlgorithmProvider provider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);

        //        CryptographicHash objHash = provider.CreateHash();

        //        IBuffer buff = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf16BE);

        //        objHash.Append(buff);

        //        IBuffer hashBuff = objHash.GetValueAndReset();

        //        string encryptedData = CryptographicBuffer.EncodeToBase64String(hashBuff);
        //        Debug.WriteLine("MD5 encrypted string :" + encryptedData);
        //        return encryptedData;
        //    }

        public static byte[] EncryptString(string data)
        {
            HashAlgorithmProvider provider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);

            CryptographicHash objHash = provider.CreateHash();

            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf16LE);

            objHash.Append(buff);

            IBuffer hashBuff = objHash.GetValueAndReset();

            string encryptedData = CryptographicBuffer.EncodeToHexString(hashBuff);
            Debug.WriteLine("MD5 encrypted string :" + encryptedData);

            byte[] bytes = new byte[32];

            
            CryptographicBuffer.CopyToByteArray(hashBuff, out bytes);

            return bytes;
        }

        /// <summary>
        /// Baozou comic's md5
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Get32MD5(string data)
        {
            char[] arrayOfChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            try
            {
                byte[] arrayOfByte = EncryptString(data);

                int k = arrayOfByte.Length;
                char[] arrayOfChar2 = new char[2 * k];

                int m = 0;
                for (int j = 0; j < k; j++)
                {
                    int n = arrayOfByte[j];
                    arrayOfChar2[m] = arrayOfChar[(0xF & n >> 4)];    //取字节中高四位进行转换
                    var x = n & 0xF;
                    arrayOfChar2[++m] = arrayOfChar[(n & 0xF)];
                    m++;
                }
                string str = new string(arrayOfChar2);
                return str;
            }
            catch (Exception e)
            {
                Debug.WriteLine("MD5Helper " + e.Message);
            }
            return null;
        }

        /// <summary>
        /// sign for login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetDictionaryForLogin(string userName,string passWord)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("timestamp", DateTimeHelper.GetUnixTimeStamp());
            dic.Add("client_id", "10230158");
            dic.Add("password", passWord);
            //dic.Add("timestamp", "1464930310788");
            dic.Add("username", userName);
           
            List<string> list = new List<string>();
            list.Add("timestamp");
            list.Add("password");
            list.Add("client_id");
            list.Add("username");

            dic.Add("sign", Get32MD5(GetMD5Sign(dic,list)));

            return dic;
        }

        public static string GetMD5Sign(Dictionary<string,string> dic,List<string> list)
        {
            string tmp;
            StringBuilder sb = new StringBuilder();
            if (dic != null && list !=null)
            {
                list.Sort();
 
                foreach (var item in dic)
                {
                    sb.Append(item.Key + "=" + item.Value);
                }
                sb.Append(GlobalValue.AccessKey);
                tmp = sb.ToString();
                tmp = WebUtility.UrlEncode(tmp);
            }
            else
            {
                tmp = string.Empty;
            }
            return tmp;
        }
    }
}
