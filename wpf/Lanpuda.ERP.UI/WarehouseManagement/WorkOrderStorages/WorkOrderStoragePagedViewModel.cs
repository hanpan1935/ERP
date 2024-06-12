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
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Edits;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Edits;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages
{
    public class WorkOrderStoragePagedViewModel : PagedViewModelBase<WorkOrderStorageDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkOrderStorageAppService _workOrderStorageAppService;
        private readonly IObjectMapper _objectMapper;
      

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string ApplyNumber
        {
            get { return GetProperty(() => ApplyNumber); }
            set { SetProperty(() => ApplyNumber, value); }
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

        public Dictionary<string, bool> IsSuccessfulSource { get; set; }


        public WorkOrderStoragePagedViewModel(
            IServiceProvider serviceProvider,
            IWorkOrderStorageAppService workOrderStorageAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _workOrderStorageAppService = workOrderStorageAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "工单入库";
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
            WorkOrderStorageEditViewModel? viewModel = _serviceProvider.GetService<WorkOrderStorageEditViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "工单入库-编辑";
                viewModel.OnCloseWindowCallbackAsync = QueryAsync;
                viewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("WorkOrderStorageEditView", viewModel);
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



        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.ApplyNumber = string.Empty;
            this.WorkOrderNumber= string.Empty;
            this.ProductName = string.Empty;
            this.IsSuccessful = null;
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
                await _workOrderStorageAppService.StoragedAsync(this.SelectedModel.Id);
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
                WorkOrderStoragePagedRequestDto requestDto = new WorkOrderStoragePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.ApplyNumber = this.ApplyNumber;
                requestDto.WorkOrderNumber = this.WorkOrderNumber;
                requestDto.ProductName = this.ProductName;
                requestDto.IsSuccessful= this.IsSuccessful;
                var result = await _workOrderStorageAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    //WorkOrderStoragePagedModel pagedModel = this._objectMapper.Map<WorkOrderStorageDto, WorkOrderStoragePagedModel>(item);
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
