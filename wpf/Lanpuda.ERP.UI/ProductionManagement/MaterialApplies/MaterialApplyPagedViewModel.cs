using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Edits;
using Lanpuda.ERP.ProductionManagement.MaterialApplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Volo.Abp.ObjectMapping;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Edits;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies
{
    public class MaterialApplyPagedViewModel : PagedViewModelBase<MaterialApplyDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMaterialApplyAppService _materialApplyAppService;
        private readonly IObjectMapper _objectMapper;

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

        public bool? IsSuccessful
        {
            get { return GetProperty(() => IsSuccessful); }
            set { SetProperty(() => IsSuccessful, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public Dictionary<string, bool> IsSuccessfulSource { get; set; }
        public Dictionary<string, bool> IsConfirmedSource { get; set; }
        #endregion


        public MaterialApplyPagedViewModel(
            IServiceProvider serviceProvider,
            IMaterialApplyAppService purchasePriceAppService,
             IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _materialApplyAppService = purchasePriceAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "领料申请";
            IsSuccessfulSource = ItemsSoureUtils.GetBoolDictionary();
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
        }


        [Command]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }

        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                MaterialApplyEditViewModel? viewModel = _serviceProvider.GetService<MaterialApplyEditViewModel>();
                if (viewModel != null)
                {
                    WindowService.Title = "领料申请-新建";
                    viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    WindowService.Show("MaterialApplyEditView", viewModel);
                }
            }
        }

        [Command]
        public void Update()
        {
            if (this.SelectedModel == null) return;

            if (this.WindowService != null)
            {
                MaterialApplyEditViewModel? viewModel = _serviceProvider.GetService<MaterialApplyEditViewModel>();
                if (viewModel != null)
                {
                    WindowService.Title = "领料申请-新建";
                    viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    viewModel.Model.Id = this.SelectedModel.Id;
                    WindowService.Show("MaterialApplyEditView", viewModel);
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
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.WorkOrderNumber = string.Empty;
            this.MpsNumber = string.Empty;
            this.ProductName = string.Empty;
            this.IsSuccessful = null;
            this.IsConfirmed = null;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null) return;
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _materialApplyAppService.DeleteAsync(this.SelectedModel.Id);
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

        [AsyncCommand]
        public async Task ConfirmeAsync()
        {
            if (this.SelectedModel == null) return;
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后将无法修改！", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _materialApplyAppService.ConfirmeAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
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

        public bool CanConfirmeAsync()
        {
            if (this.SelectedModel == null) return false;
            if (this.SelectedModel.IsConfirmed == true) return false;
            return true;
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                MaterialApplyPagedRequestDto requestDto = new MaterialApplyPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.WorkOrderNumber = this.WorkOrderNumber;
                requestDto.MpsNumber = this.MpsNumber;
                requestDto.ProductName = this.ProductName;
                requestDto.IsConfirmed = this.IsConfirmed;
                var result = await _materialApplyAppService.GetPagedListAsync(requestDto);
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
