using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    /// <summary>
    /// 设置
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// 是否自动离线下载
        /// </summary>
        public bool IsAutoCache { get; set; } = false;

        /// <summary>
        /// 是否缩略图模式
        /// </summary>
        public bool IsSmallImage { get; set; } = false;

        /// <summary>
        /// 是否文章大字号
        /// </summary>
        public bool IsBigFont { get; set; } = false;

        /// <summary>
        /// 是否新消息通知
        /// </summary>
        public bool IsNewsNotify { get; set; } = false;

        /// <summary>
        /// 缓存目录
        /// </summary>
        public string CacheFolder { get; set; }

        /// <summary>
        /// 应用版本号
        /// </summary>
        public string Version { get; set; } = $"{InformationHelper.ApplicationVersion.Major}.{InformationHelper.ApplicationVersion.Minor}.{InformationHelper.ApplicationVersion.Build}.{InformationHelper.ApplicationVersion.Revision}";
    }
}
