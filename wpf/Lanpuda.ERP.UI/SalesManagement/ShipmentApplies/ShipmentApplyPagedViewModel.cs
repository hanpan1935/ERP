using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using Lanpuda.ERP.SalesManagement.SalesOrders.Edits;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Edits;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies
{
    public class ShipmentApplyPagedViewModel : PagedViewModelBase<ShipmentApplyDto>
    {
        private readonly IShipmentApplyAppService _shipmentApplyAppService;
        private readonly IServiceProvider _serviceProvider;



        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public string CustomerName
        {
            get { return GetProperty(() => CustomerName); }
            set { SetProperty(() => CustomerName, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public Dictionary<string, bool> IsConfirmedSource { get; set; }

        public ShipmentApplyPagedViewModel(IShipmentApplyAppService shipmentApplyAppService, IServiceProvider serviceProvider)
        {
            _shipmentApplyAppService = shipmentApplyAppService;
            _serviceProvider = serviceProvider;
            this.PageTitle = "发货申请";
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
            ShipmentApplyEditViewModel? viewModel = _serviceProvider.GetService<ShipmentApplyEditViewModel>();
            if (viewModel != null)
            {
                viewModel.RefreshCallbackAsync = this.QueryAsync;
                this.WindowService.Title = "发货申请-新建";
                this.WindowService.Show("ShipmentApplyEditView", viewModel);
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            ShipmentApplyEditViewModel? viewModel = _serviceProvider.GetService<ShipmentApplyEditViewModel>();
            if (viewModel != null)
            {
                viewModel.RefreshCallbackAsync = this.QueryAsync;
                viewModel.Model.Id = this.SelectedModel.Id;
                this.WindowService.Title = "发货申请-新建";
                this.WindowService.Show("ShipmentApplyEditView", viewModel);
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


        [Command]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.CustomerName = string.Empty;
            this.IsConfirmed = null;
            await QueryAsync();
        }

        [Command]
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
                    await _shipmentApplyAppService.DeleteAsync(SelectedModel.Id);
                    await this.QueryAsync();
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

        public bool CanDeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            return true;
        }



        [AsyncCommand]
        public async Task ConfirmAsync()
        {
            if (SelectedModel == null)
            {
                return;
            }
            try
            {
                this.IsLoading = true;
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后无法编辑", caption: "提示!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await _shipmentApplyAppService.ConfirmAsync(SelectedModel.Id);
                    await this.QueryAsync();
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
        public bool CanConfirmAsync()
        {
            if (SelectedModel == null)
            {
                return false;
            }
           
            if (SelectedModel.IsConfirmed == true)
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
                ShipmentApplyPagedRequestDto requestDto = new ShipmentApplyPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.CustomerName = this.CustomerName;
                requestDto.IsConfirmed = this.IsConfirmed;
                var result = await _shipmentApplyAppService.GetPagedListAsync(requestDto);
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
