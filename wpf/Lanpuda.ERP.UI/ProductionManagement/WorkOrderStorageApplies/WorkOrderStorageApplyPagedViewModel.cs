using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.BasicData.Products.Edits;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies
{
    public class WorkOrderStorageApplyPagedViewModel : PagedViewModelBase<WorkOrderStorageApplyDto>
    {
        private readonly IWorkOrderStorageApplyAppService _workOrderStorageApplyAppService;
        private readonly IServiceProvider _serviceProvider;

        #region 搜索
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public string WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
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

        public Dictionary<string,bool> IsConfirmedSource { get; set; }

        #endregion

        public WorkOrderStorageApplyPagedViewModel(
            IWorkOrderStorageApplyAppService workOrderStorageApplyAppService,
            IServiceProvider serviceProvider)
        {
            _workOrderStorageApplyAppService = workOrderStorageApplyAppService;
            _serviceProvider = serviceProvider;
            this.PageTitle = "入库申请";
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
        }

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                WorkOrderStorageApplyEditViewModel? viewModel = (WorkOrderStorageApplyEditViewModel?)_serviceProvider.GetService(typeof(WorkOrderStorageApplyEditViewModel));
                if (viewModel != null)
                {
                    WindowService.Title = "入库申请-新建";
                    viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    WindowService.Show("WorkOrderStorageApplyEditView", viewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            if (this.WindowService != null)
            {
                WorkOrderStorageApplyEditViewModel? viewModel = (WorkOrderStorageApplyEditViewModel?)_serviceProvider.GetService(typeof(WorkOrderStorageApplyEditViewModel));
                if (viewModel != null)
                {
                    WindowService.Title = "入库申请-编辑";
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    WindowService.Show("WorkOrderStorageApplyEditView", viewModel);
                }
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed == true)
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task ConfirmeAsync()
        {
            try
            {
                if (this.SelectedModel == null) return;
                this.IsLoading = true;
                await _workOrderStorageApplyAppService.ConfirmeAsync(this.SelectedModel.Id);
                await this.QueryAsync();
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        public bool CanConfirmeAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed == true)
            {
                return false;
            }
            return true;
        }




        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.WorkOrderNumber = string.Empty;
            this.MpsNumber = string.Empty;
            this.ProductName = string.Empty;
            this.IsConfirmed = null;
            await QueryAsync();
        }

        [Command]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _workOrderStorageApplyAppService.DeleteAsync(SelectedModel.Id);
                    await this.QueryAsync();
                }
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        public bool CanDeleteAsync()
        {
            if (this.SelectedModel == null)
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
                WorkOrderStorageApplyPagedRequestDto requestDto = new WorkOrderStorageApplyPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = (this.PageIndex - 1) * DataCountPerPage;
                requestDto.Number = this.Number;
                requestDto.WorkOrderNumber = this.WorkOrderNumber;
                requestDto.IsConfirmed = this.IsConfirmed;
                var result = await _workOrderStorageApplyAppService.GetPagedListAsync(requestDto);
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
