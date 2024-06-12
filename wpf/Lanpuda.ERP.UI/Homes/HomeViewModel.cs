using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Services.SettingsServices;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.Reports;
using Lanpuda.ERP.Reports.Dtos.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Volo.Abp.Timing;

namespace Lanpuda.ERP.Homes
{
    public class HomeViewModel : RootViewModelBase
    {
        public BitmapImage Avatar { get; set; }

        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly IReportAppService _reportAppService;
        private readonly ISettingsService _settingsService;

        protected IClock Clock;

        public DateTime WorkOrderStartDate
        {
            get { return GetProperty(() => WorkOrderStartDate); }
            set { SetProperty(() => WorkOrderStartDate, value, async () => { await OnWorkOrderStartDateChangedAsync(); }); }
        }

        public string Hello
        {
            get { return GetProperty(() => Hello); }
            set { SetProperty(() => Hello, value); }
        }

        public string Surname
        {
            get { return GetProperty(() => Surname); }
            set { SetProperty(() => Surname, value); }
        }

        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }


        public string WellKnownSaying
        {
            get { return GetProperty(() => WellKnownSaying); }
            set { SetProperty(() => WellKnownSaying, value); }
        }

        public ObservableCollection<HomeWorkOrderDto> WorkOrderSource { get; set; }

        public HomeViewModel(
            IWorkOrderAppService workOrderAppService, 
            IClock clock,
            ISettingsService settingsService,
            IReportAppService reportAppService)
        {
            _workOrderAppService = workOrderAppService;
            _reportAppService = reportAppService;
            _settingsService = settingsService;
            Clock = clock;
            Avatar = _settingsService.GetUserAvatar();
            var now = DateTime.Now;
            var a = new DateTime(now.Year, now.Month, now.Day);
            var b = Clock.Normalize(a);
            WorkOrderStartDate = b;
            WorkOrderSource = new ObservableCollection<HomeWorkOrderDto>();
            this.Name = _settingsService.GetName();
            this.Surname = _settingsService.GetSurname();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                await GetWorkOrdersAsync();
                this.WellKnownSaying = GetWellKnownSaying();
                this.Hello = GetHello();
                //await Task.Delay(500);

            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        private async Task OnWorkOrderStartDateChangedAsync()
        {
            await GetWorkOrdersAsync();
        }


        private async Task GetWorkOrdersAsync()
        {
            try
            {
                this.IsLoading = true;

                DateTime o = WorkOrderStartDate;

                DateTime utc = Clock.Normalize(o);

                var homeDate = await _reportAppService.GetHomeDataAsync(utc);
                WorkOrderSource.Clear();
                foreach (var item in homeDate.WorkOrders)
                {
                    WorkOrderSource.Add(item);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        private string GetWellKnownSaying()
        {
            List<string> list = new List<string>()
            {
                "朝朝又暮暮，日有小暖，岁有小安",
                "去无人的岛摸鲨鱼的角",
                "保持须臾的浪漫 理想的喧嚣 平等的热情-徐自摩",
                "如果你停止努力，那就是谷底；如果你继续攀爬，那就是在上坡。",
                "抱怨身处黑暗，不如提灯前行。愿你在自己存在的地方，成为一束光，照亮世界的一角",
                "细枝末节累加起来，即是生活",
                "人闲桂花落，满身都是秋。                                           ",
                "盛意已山河，山河不及你。                                           ",
                "世界先爱了我，我不能不爱它。                                       ",
                "苦练七十二变，笑对八十一难。                                       ",
                "失落时悄悄伸出手和风击个掌。                                       ",
                "做颗星星，有棱有角，还会发光。                                     ",
                "上了生活的贼船，就做快乐的海盗。                                   ",
                "梦里走了千万里，醒来还是在床上。                                   ",
                "哪里有爱，哪里就有不顾一切的信任。                                 ",
                "人生行路，芬芳之人方能遇到芬芳的心。                               ",
                "交友须带三分侠气，做人要存一点素心。                               ",
                "认真生活就能找到生活里藏起来的糖果。                               ",
                "零星的变好，最后也会和星河一样闪耀。                               ",
                "只要有花可开，就不允许生命与黯淡为伍。                             ",
                "先努力让自己发光，对的人才能迎着光而来。                           ",
                "人生一世，草生一春，来如风雨，去似微尘。                           ",
                "如果你喜欢，它就是喜悦，是意境，是海棠花里寻往昔。                 ",
                "错过落日余晖，请记得还有漫天星辰。                                 ",
                "每个人的裂纹最后都会变成故事的花纹。                               ",
                "要去追寻月亮，即使坠落也是掉进浩瀚星河。                           ",
                "我抓不住这世间的美好，只好装作万事顺遂的模样。                     ",
                "夕阳总会落在你身上，你也会有自己的宇航员和月亮。                   ",
                "努力的最大意义是让自己随时有能力跳出自己厌恶的圈子。               ",
                "尘世是非，躲不开人间风月。人间风月，躲不开情深意长。               ",
                "习惯于绝望的处境比绝望处境本身还要糟。——加缪《鼠疫》               ",
                "最好的生活状态就是，一个人时，安静而丰盛，两个人时，温暖而踏实。   ",
                "在长长的沉默之后说出的话，原本根本就不愿说。——赫塔·米勒《呼吸秋千》",
            };

            Random r = new Random();
            int index = r.Next(0, list.Count);
            return list[index];
        }

        private string GetHello()
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;

            if (hour >= 6  && hour <9 )
            {
                return "早安!";
            }
            if (hour >= 9 && hour <12)
            {
                return "早上好!";
            }

            if (hour >= 12 && hour < 14)
            {
                return "中午好!";
            }
            if (hour >= 14 && hour < 18)
            {
                return "下午好!";
            }
            if (hour >= 18 && hour < 22)
            {
                return "晚上好!";
            }
            if (hour >= 22 && hour < 6)
            {
                return "晚安!";
            }
            return "";
        }
    }
}
