using BaoZouRiBao.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class SignHelperTest
    {
        [TestMethod]
        public void Sign()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "timestamp", DateTimeHelper.GetUnixTimeStamp() },
                { "client_id", "10230158" },
                { "password", "qwertyx123" },
                { "timestamp", DateTimeHelper.GetUnixTimeStamp() },
                { "username", "arcsinw" }
            };

            List<string> list = new List<string>
            {
                "timestamp",
                "password",
                "client_id",
                "username"
            };

            string actual = UrlSignHelper.GetMD5Sign(dic, list);
            string expected = "34c08b123b17b298217988aeae6d97cf";
            Assert.AreEqual(expected, actual);

            //dic.Add("sign", MD5Helper.Get32MD5(MD5Helper.GetMD5Sign(dic, list)));

            //string expected = "34c08b123b17b298217988aeae6d97cf";
            //string actual = MD5Helper.Get32MD5("1496390654358");

            Assert.AreEqual(dic["sign"], "34c08b123b17b298217988aeae6d97cf");
        }
    }
}
