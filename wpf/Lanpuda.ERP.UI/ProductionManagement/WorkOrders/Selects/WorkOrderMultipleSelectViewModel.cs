using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using HandyControl.Controls;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Selects
{
    public class WorkOrderMultipleSelectViewModel : PagedViewModelBase<WorkOrderDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkOrderAppService _workOrderAppService;
        public Action<ICollection<WorkOrderDto>>? OnSelectedCallback;
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        public ObservableCollection<WorkOrderDto> SelectedWorkOrders { get; set; }

        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }


        public WorkOrderMultipleSelectViewModel(
            IServiceProvider serviceProvider,
            IWorkOrderAppService workOrderAppService
            )
        {
            _serviceProvider = serviceProvider;
            _workOrderAppService = workOrderAppService;
            SelectedWorkOrders = new ObservableCollection<WorkOrderDto>();
            this.PageTitle = "选择-生产工单";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public async Task ResetAsync()
        {
            this.Name = String.Empty;
            await QueryAsync();
        }

        [Command]
        public void Select()
        {
            if (this.SelectedModel == null) return;
            var isExists = this.SelectedWorkOrders.Any(m => m.Id == SelectedModel.Id);
            if (isExists)
            {
                Growl.Info("已经添加了，不能重复添加");
                return;
            }
            
            this.SelectedWorkOrders.Add(SelectedModel);
            
        }

        [Command]
        public void Save()
        {
            if (this.OnSelectedCallback != null)
            {
                OnSelectedCallback(this.SelectedWorkOrders);
                this.CurrentWindowService.Close();
            }
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderPagedRequestDto requestDto = new WorkOrderPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                var result = await _workOrderAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

    }
}
