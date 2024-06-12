using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.BulkCreates;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories
{
    public class SafetyInventoryPagedViewModel : PagedViewModelBase<SafetyInventoryDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISafetyInventoryAppService _safetyInventoryAppService;


        public string ProductName
        {
            get { return GetValue<string>(nameof(ProductName)); }
            set { SetValue(value, nameof(ProductName)); }
        }

        public Guid? ProductId
        {
            get { return GetProperty(() => ProductId); }
            private set { SetProperty(() => ProductId, value); }
        }


        public SafetyInventoryPagedViewModel(IServiceProvider serviceProvider, ISafetyInventoryAppService safetyInventoryAppService)
        {
            _serviceProvider = serviceProvider;
            _safetyInventoryAppService = safetyInventoryAppService;
            this.PageTitle = "安全库存";

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
                SafetyInventoryBulkCreateViewModel? viewModel = _serviceProvider.GetService<SafetyInventoryBulkCreateViewModel>();
                if (viewModel != null)
                {
                    viewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "安全库存-批量新建";
                    WindowService.Show("SafetyInventoryBulkCreateView", viewModel);
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
                SafetyInventoryEditViewModel? viewModel = _serviceProvider.GetService<SafetyInventoryEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = SelectedModel.Id;
                    viewModel.RefreshCallbacAsync = this.QueryAsync;
                    WindowService.Title = "安全库存-编辑";
                    WindowService.Show("SafetyInventoryEditView", viewModel);
                }
            }
        }



        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.ProductName = String.Empty;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _safetyInventoryAppService.DeleteAsync(this.SelectedModel.Id);
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


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                SafetyInventoryPagedRequestDto requestDto = new SafetyInventoryPagedRequestDto();
                requestDto.ProductName = this.ProductName;
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.ProductId = this.ProductId;
                requestDto.ProductName = this.ProductName;
                var result = await _safetyInventoryAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify= false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify= true;
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
