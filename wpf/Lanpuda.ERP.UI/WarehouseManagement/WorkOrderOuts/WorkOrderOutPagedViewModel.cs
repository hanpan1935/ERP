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
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Edits;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Edits;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts
{
    public class WorkOrderOutPagedViewModel : PagedViewModelBase<WorkOrderOutDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkOrderOutAppService _workOrderOutAppService;
        private readonly IObjectMapper _objectMapper;

        #region 搜索


        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string MaterialApplyNumber
        {
            get { return GetProperty(() => MaterialApplyNumber); }
            set { SetProperty(() => MaterialApplyNumber, value); }
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


        public WorkOrderOutPagedViewModel(
            IServiceProvider serviceProvider,
            IWorkOrderOutAppService workOrderOutAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _workOrderOutAppService = workOrderOutAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "生产领料";
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
            WorkOrderOutEditViewModel? viewModel = _serviceProvider.GetService<WorkOrderOutEditViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "生产领料-编辑";
                viewModel.OnCloseWindowCallbackAsync = QueryAsync;
                viewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("WorkOrderOutEditView", viewModel);
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
            this.MaterialApplyNumber = string.Empty;
            this.WorkOrderNumber = string.Empty;
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
                await _workOrderOutAppService.OutedAsync(this.SelectedModel.Id);
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
                WorkOrderOutPagedRequestDto requestDto = new WorkOrderOutPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.MaterialApplyNumber= this.MaterialApplyNumber;
                requestDto.WorkOrderNumber = this.WorkOrderNumber;
                requestDto.IsSuccessful = this.IsSuccessful;
                requestDto.ProductName = this.ProductName;
                var result = await _workOrderOutAppService.GetPagedListAsync(requestDto);
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
