using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Volo.Abp.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Edits;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Edits;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns
{
    public class WorkOrderReturnPagedViewModel : PagedViewModelBase<WorkOrderReturnDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkOrderReturnAppService _workOrderOutAppService;
        private readonly IObjectMapper _objectMapper;


      
        #region 搜索

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string MaterialReturnApplyNumber
        {
            get { return GetProperty(() => MaterialReturnApplyNumber); }
            set { SetProperty(() => MaterialReturnApplyNumber, value); }
        }

        public string WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public bool? IsSuccessful
        {
            get { return GetProperty(() => IsSuccessful); }
            set { SetProperty(() => IsSuccessful, value); }
        }


        public Dictionary<string,bool> IsSuccessfulSource { get; set; }

        #endregion


        public WorkOrderReturnPagedViewModel(
            IServiceProvider serviceProvider,
            IWorkOrderReturnAppService purchaseReturnAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _workOrderOutAppService = purchaseReturnAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "生产退料";
            IsSuccessfulSource = ItemsSoureUtils.GetBoolDictionary();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }

       

        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            WorkOrderReturnEditViewModel? viewModel = _serviceProvider.GetService<WorkOrderReturnEditViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "生产退料-编辑";
                viewModel.OnCloseWindowCallbackAsync = QueryAsync;
                viewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("WorkOrderReturnEditView", viewModel);
            }
        }


        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }

            if (this.SelectedModel.IsSuccessful != false)
            {
                return false;
            }
            return true;
        }

    

        [Command]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.MaterialReturnApplyNumber = String.Empty;
            this.WorkOrderNumber = String.Empty;
            this.IsSuccessful = null;
            this.ProductName = string.Empty;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task StoragedAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                this.IsLoading = true;
                await _workOrderOutAppService.StoragedAsync(this.SelectedModel.Id);
                await this.QueryAsync();
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

        public bool CanStoragedAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsSuccessful == true)
            {
                return false;
            }
            if (this.SelectedModel.Details.Count == 0)
            {
                return false;
            }
            return true;
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderReturnPagedRequestDto requestDto = new WorkOrderReturnPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.MaterialReturnApplyNumber = this.MaterialReturnApplyNumber;
                requestDto.WorkOrderNumber = this.WorkOrderNumber;
                requestDto.ProductName = this.ProductName;
                requestDto.IsSuccessful = this.IsSuccessful;
                var result = await _workOrderOutAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                PagedDatas.CanNotify = true;
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
