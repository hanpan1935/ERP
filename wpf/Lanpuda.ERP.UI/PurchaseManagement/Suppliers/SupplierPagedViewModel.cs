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
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Edits;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Edits;
using Microsoft.Extensions.DependencyInjection;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers
{
    public class SupplierPagedViewModel : PagedViewModelBase<SupplierDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISupplierAppService _supplierAppService;

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }
        public string ShortName
        {
            get { return GetProperty(() => ShortName); }
            set { SetProperty(() => ShortName, value); }
        }




        public SupplierPagedViewModel(IServiceProvider serviceProvider, ISupplierAppService supplierAppService)
        {
            _serviceProvider = serviceProvider;
            _supplierAppService = supplierAppService;
            this.PageTitle = "供应商信息";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            SupplierEditViewModel? viewModel = _serviceProvider.GetService<SupplierEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Refresh = this.QueryAsync;
                WindowService.Title = "供应商-新建";
                WindowService.Show(typeof(SupplierEditView).FullName, viewModel);
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            SupplierEditViewModel? viewModel = _serviceProvider.GetService<SupplierEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Refresh = this.QueryAsync;
                viewModel.Model.Id = SelectedModel.Id;
                WindowService.Title = "供应商-新建";
                WindowService.Show("SupplierEditView", viewModel);
            }
        }

       


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.FullName = String.Empty;
            this.ShortName = String.Empty;
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
                    await _supplierAppService.DeleteAsync(SelectedModel.Id);
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
                SupplierPagedRequestDto requestDto = new SupplierPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.FullName = this.FullName;
                requestDto.ShortName = this.ShortName;
                var result = await _supplierAppService.GetPagedListAsync(requestDto);
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
