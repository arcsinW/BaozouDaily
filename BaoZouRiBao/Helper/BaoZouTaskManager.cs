using BaoZouRiBao.Enums;
using BaoZouRiBao.Http;
using BaoZouRiBao.Views;
using System;
using Windows.Storage;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public class BaoZouTaskManager
    {
        public BaoZouTaskManager()
        {
            if (IsNewDay())
            {
                IsSigned = false;
                IsDocumentVoted = false;
                IsCommentVoted = false;
                IsRead = false;
                IsCommented = false;
                IsShared = false;

                previousDate = DateTime.Now.Date;
            }
            else
            {
                LoadSettings();
            }
        }

        private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        #region Keys
        private const string IsSigned_Key = "IsSigned";
        private const string IsDocumentVoted_Key = "IsDocumentVoted";
        private const string IsCommentVoted_Key = "IsCommentVoted";
        private const string IsRead_Key = "IsRead";
        private const string IsCommented_Key = "IsCommented";
        private const string IsShared_Key = "IsShared";

        /// <summary>
        /// 上一次日期
        /// </summary>
        private const string PreviousDate_Key = "PreviousDate";
        #endregion

        #region Fields
        private bool isSigned = false;
        /// <summary>
        /// 是否签到
        /// </summary>
        private bool IsSigned
        {
            get
            {
                return isSigned;
            }
            set
            {
                isSigned = (bool)GetValue(localSettings, false, IsSigned_Key);
            }
        }

        private bool isDocumentVoted = false;
        /// <summary>
        /// 是否点赞文章
        /// </summary>
        private bool IsDocumentVoted
        {
            get
            {
                return isSigned;
            }
            set
            {
                isDocumentVoted = (bool)GetValue(localSettings, false, IsDocumentVoted_Key);
            }
        }

        private bool isCommentVoted = false;
        /// <summary>
        /// 是否点赞评论
        /// </summary>
        private bool IsCommentVoted
        {
            get
            {
                return isCommentVoted;
            }
            set
            {
                isCommentVoted = (bool)GetValue(localSettings, false, IsCommented_Key);
            }
        }

        private bool isRead = false;
        /// <summary>
        /// 是否阅读过文章
        /// </summary>
        private bool IsRead
        {
            get
            {
                return isRead;
            }
            set
            {
                isRead = (bool)GetValue(localSettings, false, IsRead_Key);
            }
        }

        private bool isCommented = false;
        /// <summary>
        /// 是否评论过文章
        /// </summary>
        private bool IsCommented
        {
            get
            {
                return isCommented;
            }
            set
            {
                isCommented = (bool)GetValue(localSettings, false, IsCommented_Key);
            }
        }

        private bool isShared = false;
        /// <summary>
        /// 是否分享过文章
        /// </summary>
        private bool IsShared
        {
            get
            {
                return isShared;
            }
            set
            {
                isShared = (bool)GetValue(localSettings, false, IsShared_Key);
            }
        }

        private static DateTime previousDate = DateTime.MinValue;
        /// <summary>
        /// 上次日期
        /// 检测到当前日期和此日期不同时重置所有的任务完成信息
        /// </summary>
        private static DateTime PreviousDate
        {
            get
            {
                return previousDate;
            }
            set
            {
                previousDate = (DateTime)GetValue(localSettings, DateTime.MinValue, PreviousDate_Key);
            }
        }
        #endregion

        /// <summary>
        /// 加载任务完成状态
        /// </summary>
        public void LoadSettings()
        {
            IsSigned = (bool)GetValue(localSettings, false, IsSigned_Key);
            IsDocumentVoted = (bool)GetValue(localSettings, false, IsDocumentVoted_Key);
            IsCommentVoted = (bool)GetValue(localSettings, false, IsCommentVoted_Key);
            IsRead = (bool)GetValue(localSettings, false, IsRead_Key);
            IsCommented = (bool)GetValue(localSettings, false, IsCommented_Key);
            IsShared = (bool)GetValue(localSettings, false, IsShared_Key);
        }

        /// <summary>
        /// 安全地从LocalSettings中获取值
        /// </summary>
        /// <param name="set">数据容器</param>
        /// <param name="defaultValue">不存在时的默认值</param>
        /// <param name="key">数据的Key</param>
        /// <returns></returns>
        private static object GetValue(ApplicationDataContainer set, object defaultValue, string key)
        {
            if (set.Values.ContainsKey(key))
            {
                return set.Values[key];
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 新的一天？
        /// Makes no sense if you're not here
        /// </summary>
        /// <returns></returns>
        private static bool IsNewDay()
        {
            if (!DateTime.Parse((string)localSettings.Values[PreviousDate_Key]).Equals(DateTime.Now.Date.ToString()))
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
            if (DataShareManager.Current.User != null && !string.IsNullOrEmpty(DataShareManager.Current.User.AccessToken))
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
            if (IsLogin() && IsNewDay())
            {
                var result = await ApiService.Instance.TaskDoneAsync(BaoZouTaskEnum.DailySign);
                if (result != null && result.Status.Equals("success"))
                {
                    MasterDetailPage.Current.ShowTaskDialog(result);

                    // 更新上次签到日期
                    
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
        /// 点赞评论
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
