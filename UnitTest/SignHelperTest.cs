using BaoZouRiBao.Helper;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
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
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("timestamp", DateTimeHelper.GetUnixTimeStamp());
            dic.Add("client_id", "10230158");
            dic.Add("password", "qwertyx123");
            dic.Add("timestamp", DateTimeHelper.GetUnixTimeStamp());
            dic.Add("username", "arcsinw");

            List<string> list = new List<string>();
            list.Add("timestamp");
            list.Add("password");
            list.Add("client_id");
            list.Add("username");

            dic.Add("sign", MD5Helper.Get32MD5(MD5Helper.GetMD5Sign(dic, list)));

            //string expected = "34c08b123b17b298217988aeae6d97cf";
            //string actual = MD5Helper.Get32MD5("1496390654358");

            Assert.AreEqual(dic["sign"], "34c08b123b17b298217988aeae6d97cf");
        }
    }
}
