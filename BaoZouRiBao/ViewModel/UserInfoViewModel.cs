using BaoZouRiBao.Controls;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
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
                LoadData();
            }
            User = DataShareManager.Current.User;
            DataShareManager.Current.DataChanged += Current_DataChanged; 
        }

        ~UserInfoViewModel()
        {
            DataShareManager.Current.DataChanged -= Current_DataChanged;
        }

        private void Current_DataChanged()
        {
            User = DataShareManager.Current.User;
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

        private async Task LoadTaskInfo()
        {
            TaskInfo = await ApiService.Instance.GetTaskInfoAsync();
            if (TaskInfo != null)
            {
                Balance = TaskInfo.Balance;
            }
        }

        public override async void Refresh()
        {
            IsActive = true; 
            await LoadTaskInfo();
            IsActive = false;
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ModifyAvatar(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".bmp");
            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                ModifyAvatarResult result = await UploadImage(ServiceUri.UploadAvatar, file);
                if (result != null)
                {
                    User.Avatar = result.Avatar;
                    ToastService.SendToast("头像更新成功");
                }
            }
        }

        private void LoadDesignData()
        {
            string json = "{'balance':0,'favorite_count':0,'comment_count':0,'article_read_count':0,'contribute_count':0,'daily_tasks':[{'task_id':1,'name':'每日签到','task_amount':null,'task_amount_min':5,'task_amount_max':15,'amount':13,'done':true,'increase':true},{'task_id':2,'name':'阅读文章','task_amount':5,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false},{'task_id':6,'name':'评论文章','task_amount':10,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false},{'task_id':4,'name':'点赞文章','task_amount':5,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false},{'task_id':5,'name':'点赞评论','task_amount':5,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false},{'task_id':3,'name':'分享文章','task_amount':10,'task_amount_min':null,'task_amount_max':null,'amount':0,'done':false,'increase':false}]}";
            var taskInfo = JsonHelper.Deserlialize<TaskInfo>(json);
            TaskInfo = taskInfo;
        }

        private async void LoadData()
        {
            await LoadTaskInfo();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<ModifyAvatarResult> UploadImage(string url, StorageFile file)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + DataShareManager.Current.User.AccessToken);
                    client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                    client.DefaultRequestHeaders.Add("User-Agent", "(Linux; Android 5.0.2; vivo X5M Build/vivo/PD1401CL/PD1401CL/LRX22G/zh_CN)");

                    using (var content = new MultipartFormDataContent("TLq-mXb4y62pbCa_bPiNZXitxUS3RV29c8"))
                    {
                        var imageContent = new StreamContent(await file.OpenStreamForReadAsync());

                        string boundary = Guid.NewGuid().ToString();

                        content.Headers.Add("ContentType", "multipart/form-data; boundary=TLq-mXb4y62pbCa_bPiNZXitxUS3RV29c8");

                        if (file.Name.EndsWith(".jpg"))
                        {
                            content.Add(imageContent, "avatar", "avatar.jpg");
                            content.Headers.TryAddWithoutValidation("ContentType", "jpg:image/jpg");
                        }
                        else if (file.Name.EndsWith(".png"))
                        {
                            content.Add(imageContent, "avatar", "avatar.png");
                            content.Headers.TryAddWithoutValidation("ContentType", "png:image/png");
                        }

                        content.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse("form-data; name='avatar'; filename='" + file.Name + "'");

                        var result = await client.PostAsync(new Uri(url), content);
                        var resContent = await result.Content.ReadAsByteArrayAsync();
                        string json = Encoding.UTF8.GetString(resContent);
                        ModifyAvatarResult avatarResult = JsonHelper.Deserlialize<ModifyAvatarResult>(json);
                        return avatarResult;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
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
