using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace BaoZouRiBao.ViewModel
{
    public class UserInfoViewModel : ViewModelBase
    {
        public UserInfoViewModel()
        {
            TaskInfo = new TaskInfo();
            if(IsDesignMode)
            {
                LoadDesignData();
            }
            else
            {
                LoadTaskInfo();
            }
            User = GlobalValue.Current.User;
            GlobalValue.Current.DataChanged += Current_DataChanged; 
        }

        private void Current_DataChanged()
        {
            User = GlobalValue.Current.User;
        }

        #region Properties

        private User user;

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private TaskInfo taskInfo;

        public TaskInfo TaskInfo
        {
            get
            {
                return taskInfo;
            }
            set
            {
                taskInfo = value;
                OnPropertyChanged();
            }
        }

        private int balance;

        public int Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
                OnPropertyChanged();
            }
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        #endregion

        private async void LoadTaskInfo()
        {
            TaskInfo = await ApiService.Instance.GetTaskInfo();
            if (TaskInfo != null)
            {
                RunCounterAnimations();
            }
        }

        private void LoadDesignData()
        {
            string json = "{'balance':0,'favorite_count':0,'comment_count':0,'article_read_count':0,'contribute_count':0,'daily_tasks':[{'task_id':1,'name':'每日签到','task_amount':null,'task_amount_min':5,'task_amount_max':15,'amount':13,'done':true,'increase':true},{'task_id':2,'name':'阅读文章','task_amount':5,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false},{'task_id':6,'name':'评论文章','task_amount':10,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false},{'task_id':4,'name':'点赞文章','task_amount':5,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false},{'task_id':5,'name':'点赞评论','task_amount':5,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false},{'task_id':3,'name':'分享文章','task_amount':10,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false}]}";
            var taskInfo = JsonHelper.Deserlialize<TaskInfo>(json);
            TaskInfo = taskInfo;
        }

        private void RunCounterAnimations()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = TaskInfo.Balance;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(10));

            var dispatcherTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(0.001) };
            dispatcherTimer.Tick += (s, e) =>
              {
                  var needToProceed = false;

                  if (Balance < TaskInfo.Balance)
                  {
                      Balance++;
                      needToProceed = true;
                  }

                  if (!needToProceed)
                  {
                      dispatcherTimer.Stop();
                  }
              };
            dispatcherTimer.Start();
        }
    }
}
