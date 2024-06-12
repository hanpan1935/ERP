using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Edits;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using System.Drawing.Printing;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Selects
{
    public class WorkOrderSelectViewModel : PagedViewModelBase<WorkOrderDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkOrderAppService _workOrderAppService;
        public Action<WorkOrderDto>? OnSelectedCallback;
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        #region 搜索
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string MpsNumber
        {
            get { return GetProperty(() => MpsNumber); }
            set { SetProperty(() => MpsNumber, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }
        /// <summary>
        /// 开工时间
        /// </summary>
        public DateTime? StartDate
        {
            get { return GetProperty(() => StartDate); }
            set { SetProperty(() => StartDate, value); }
        }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompletionDate
        {
            get { return GetProperty(() => CompletionDate); }
            set { SetProperty(() => CompletionDate, value); }
        }
      
        #endregion

        public WorkOrderSelectViewModel(
            IServiceProvider serviceProvider, 
            IWorkOrderAppService workOrderAppService
            )
        {
            _serviceProvider = serviceProvider;
            _workOrderAppService = workOrderAppService;
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
            this.Number = string.Empty;
            this.MpsNumber = string.Empty;
            this.ProductName = string.Empty;
            this.StartDate = null;
            this.CompletionDate = null;
            await QueryAsync();
        }

        [Command]
        public void Select()
        {
            if (this.SelectedModel == null) return;
            if (this.OnSelectedCallback == null) return;
            OnSelectedCallback(SelectedModel);

            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderPagedRequestDto requestDto = new WorkOrderPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.MpsNumber = this.MpsNumber;
                requestDto.ProductName = this.ProductName;
                requestDto.IsConfirmed = this.IsConfirmed;
                requestDto.StartDate = this.StartDate;
                requestDto.CompletionDate = this.CompletionDate;

                var result = await _workOrderAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify = true;
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
