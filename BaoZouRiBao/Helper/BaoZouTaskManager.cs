using BaoZouRiBao.Enums;
using BaoZouRiBao.Http;
using BaoZouRiBao.Views;
using System;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// 每日任务管理器
    /// </summary>
    public class BaoZouTaskManager
    {
        /// <summary>
        /// 上次签到日期
        /// </summary>
        private string LastSignDate = string.Empty;
        
        /// <summary>
        /// 是否今天首次签到
        /// </summary>
        /// <returns></returns>
        private static bool IsFirstSign()
        {
            if(!GlobalValue.Current.LastSignDate.Equals(DateTime.Now.Date.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否已经登录
        /// </summary>
        /// <returns></returns>
        private static bool IsLogin()
        {
            if (GlobalValue.Current.User != null && !string.IsNullOrEmpty(GlobalValue.Current.User.AccessToken))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 每日签到
        /// 每天首次打开App时完成
        /// </summary>
        public static async void DailySign()
        {
            if (IsLogin() && IsFirstSign())
            {
                var result = await ApiService.Instance.TaskDoneAsync(BaoZouTaskEnum.DailySign);
                if (result != null && result.Status.Equals("success"))
                {
                    MasterDetailPage.Current.ShowTaskDialog(result);
                    GlobalValue.Current.UpdateLastSignDate(DateTime.Now.Date.ToString());
                }
            }
        }

        /// <summary>
        /// 阅读文章
        /// </summary>
        public static async void ReadDocument()
        {
            var result = await ApiService.Instance.TaskDoneAsync(BaoZouTaskEnum.ReadDocument);
            if (result != null)
            {
                MasterDetailPage.Current.ShowTaskDialog(result);
            }
        }

        /// <summary>
        /// 评论文章
        /// </summary>
        public static async void CommentDocument()
        {
            var result = await ApiService.Instance.TaskDoneAsync(BaoZouTaskEnum.CommentDocument);
            if (result != null)
            {
                MasterDetailPage.Current.ShowTaskDialog(result);
            }
        }

        /// <summary>
        /// 完成点赞文章任务
        /// </summary>
        public static async void VoteDocument()
        {
            var result = await ApiService.Instance.TaskDoneAsync(BaoZouTaskEnum.VoteDocument);
            if (result != null)
            {
                MasterDetailPage.Current.ShowTaskDialog(result);
            }
        }

        /// <summary>
        /// 点赞文章
        /// </summary>
        public static async void VoteComment()
        {
            var result = await ApiService.Instance.TaskDoneAsync(BaoZouTaskEnum.VoteComment);
            if (result != null)
            {
                MasterDetailPage.Current.ShowTaskDialog(result);
            }
        }

        /// <summary>
        /// 分享文章
        /// </summary>
        public static async void ShareDocument()
        {
            var result = await ApiService.Instance.TaskDoneAsync(BaoZouTaskEnum.ShareDocument);
            if (result != null)
            {
                MasterDetailPage.Current.ShowTaskDialog(result);
            }
        }
    }
}
