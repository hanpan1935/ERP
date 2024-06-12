using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using HandyControl.Tools.Extension;
using System.Windows.Forms;
using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;
using System.Windows.Controls;
using Lanpuda.ERP.SalesManagement.SalesPrices.Edits;
using Lanpuda.ERP.SalesManagement.SalesOrders.Edits;
using Lanpuda.ERP.SalesManagement.SalesOrders.CreateMpses;
using Lanpuda.ERP.SalesManagement.SalesOrders.Profiles;

namespace Lanpuda.ERP.SalesManagement.SalesOrders
{
    public class SalesOrderPagedViewModel : PagedViewModelBase<SalesOrderDto>
    {
        private readonly ISalesOrderAppService _salesOrderAppService;
        private readonly IServiceProvider _serviceProvider;

        public SalesOrderPagedRequestModel RequestModel { get; set; }


        public SalesOrderPagedViewModel(ISalesOrderAppService salesOrderAppService, IServiceProvider serviceProvider)
        {
            _salesOrderAppService = salesOrderAppService;
            _serviceProvider = serviceProvider;
            this.PageTitle = "销售订单";
            RequestModel = new SalesOrderPagedRequestModel();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            SalesOrderEditViewModel? viewModel = _serviceProvider.GetService<SalesOrderEditViewModel>();
            if (viewModel != null)
            {
                viewModel.RefreshCallbackAsync = this.QueryAsync;
                this.WindowService.Title = "销售订单-新建";
                this.WindowService.Show("SalesOrderEditView", viewModel);
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            SalesOrderEditViewModel? viewModel = _serviceProvider.GetService<SalesOrderEditViewModel>();
            if (viewModel != null)
            {
                viewModel.RefreshCallbackAsync = this.QueryAsync;
                viewModel.Model.Id = this.SelectedModel.Id;
                this.WindowService.Title = "销售订单-编辑";
                this.WindowService.Show("SalesOrderEditView", viewModel);

            }
        }


        public bool CanUpdate()
        {

            if (this.SelectedModel == null)
            {
                return false;
            }
            if (SelectedModel.IsConfirmed == true)
            {
                return false;
            }
            return true;
        }


        [Command]
        public void Profile()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            SalesOrderProfileViewModel? viewModel = _serviceProvider.GetService<SalesOrderProfileViewModel>();
            if (viewModel != null)
            {
                viewModel.Id = this.SelectedModel.Id;
                this.WindowService.Title = "销售订单-明细";
                this.WindowService.Show("SalesOrderProfileView", viewModel);
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.RequestModel.Number = String.Empty;
            this.RequestModel.CustomerId = null;
            this.RequestModel.CustomerName = String.Empty;
            this.RequestModel.RequiredDateStart = null;
            this.RequestModel.RequiredDateEnd = null;
            this.RequestModel.PromisedDateStart = null;
            this.RequestModel.PromisedDateEnd = null;
            this.RequestModel.OrderType = null;
            this.RequestModel.IsConfirmed = null;
            this.RequestModel.CloseStatus = null;
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
                    await _salesOrderAppService.DeleteAsync(SelectedModel.Id);
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


        [AsyncCommand]
        public async Task ConfirmAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要确认吗?确认后无法编辑修改!", caption: "提示!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _salesOrderAppService.ConfirmAsync(SelectedModel.Id);
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

        public bool CanConfirmAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (SelectedModel.IsConfirmed == true)
            {
                return false;
            }
            return true;
        }

        [AsyncCommand]
        public async Task CloseOrderAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                this.IsLoading = true;
                await _salesOrderAppService.CloseOrderAsync(this.SelectedModel.Id);
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

        public bool CanCloseOrderAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.CloseStatus != SalesOrderCloseStatus.ToBeClosed)
            {
                return false;
            }
            return true;
        }

        [AsyncCommand]
        public async Task CreateMpsAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要生成生产计划吗?", caption: "提示!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _salesOrderAppService.CreateMpsAsync(this.SelectedModel.Id);
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

        public bool CanCreateMpsAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel .IsConfirmed == false)
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
                SalesOrderPagedRequestDto requestDto = new SalesOrderPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = RequestModel.Number;
                requestDto.CustomerId = RequestModel.CustomerId;
                requestDto.CustomerName = RequestModel.CustomerName;
                requestDto.RequiredDateStart = RequestModel.RequiredDateStart;
                requestDto.RequiredDateEnd = RequestModel.RequiredDateEnd;
                requestDto.PromisedDateStart = RequestModel.PromisedDateStart;
                requestDto.PromisedDateEnd = RequestModel.PromisedDateEnd;
                requestDto.OrderType = RequestModel.OrderType;
                requestDto.IsConfirmed = RequestModel.IsConfirmed;
                requestDto.CloseStatus = RequestModel.CloseStatus;
                var result = await _salesOrderAppService.GetPagedListAsync(requestDto);
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
