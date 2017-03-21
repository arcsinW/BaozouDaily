using BaoZouRiBao.Enums;
using BaoZouRiBao.Http;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// 每日任务管理器
    /// </summary>
    public class BaoZouTaskManager
    {
        /// <summary>
        /// 每日签到
        /// 每天首次打开App时完成
        /// </summary>
        public static async void DailySign()
        {
            var result = await ApiService.Instance.TaskDone(BaoZouTaskEnum.DailySign.ToString());
            if (result == null) return;
            MasterDetailPage.Current.ShowTaskPopup(result);
        }

        /// <summary>
        /// 阅读文章
        /// </summary>
        public static async void ReadDocument()
        {
            var result = await ApiService.Instance.TaskDone(BaoZouTaskEnum.ReadDocument.ToString());
            if (result == null) return;
            MasterDetailPage.Current.ShowTaskPopup(result);
        }

        /// <summary>
        /// 评论文章
        /// </summary>
        public static async void CommentDocument()
        {
            var result = await ApiService.Instance.TaskDone(BaoZouTaskEnum.CommentDocument.ToString());
            if (result == null) return;
            MasterDetailPage.Current.ShowTaskPopup(result);
        }

        /// <summary>
        /// 点赞文章
        /// </summary>
        public static async void VoteDocument()
        {
            var result = await ApiService.Instance.TaskDone(BaoZouTaskEnum.VoteDocument.ToString());
            if (result == null) return;
            MasterDetailPage.Current.ShowTaskPopup(result);
        }

        /// <summary>
        /// 点赞文章
        /// </summary>
        public static async void VoteComment()
        {
            var result = await ApiService.Instance.TaskDone(BaoZouTaskEnum.VoteComment.ToString());
            if (result == null) return;
            MasterDetailPage.Current.ShowTaskPopup(result);
        }

        /// <summary>
        /// 分享文章
        /// </summary>
        public static async void ShareDocument()
        {
            var result = await ApiService.Instance.TaskDone(BaoZouTaskEnum.ShareDocument.ToString());
            if (result == null) return;
            MasterDetailPage.Current.ShowTaskPopup(result);
        }
    }
}
