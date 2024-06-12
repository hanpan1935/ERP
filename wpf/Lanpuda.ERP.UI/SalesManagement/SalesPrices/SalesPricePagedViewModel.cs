using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;
using Lanpuda.ERP.SalesManagement.SalesPrices.Edits;
using Lanpuda.ERP.SalesManagement.SalesPrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Windows.Themes;

namespace Lanpuda.ERP.SalesManagement.SalesPrices
{
    public class SalesPricePagedViewModel : PagedViewModelBase<SalesPriceDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISalesPriceAppService _salesPriceAppService;


        #region 搜索
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

        public DateTime? ValidDate
        {
            get { return GetProperty(() => ValidDate); }
            set 
            {
                SetProperty(() => ValidDate, value); 
            }
        }


       
        #endregion


        public SalesPricePagedViewModel(IServiceProvider serviceProvider, ISalesPriceAppService salesPriceAppService)
        {
            _serviceProvider = serviceProvider;
            _salesPriceAppService = salesPriceAppService;
            this.PageTitle = "销售报价";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            SalesPriceEditViewModel? viewModel = _serviceProvider.GetService<SalesPriceEditViewModel>();
            if (viewModel != null)
            {
                viewModel.RefreshCallbackAsync = this.QueryAsync;
                this.WindowService.Title = "销售报价-新建";
                this.WindowService.Show("SalesPriceEditView", viewModel);

            }
        }



        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            SalesPriceEditViewModel? viewModel = _serviceProvider.GetService<SalesPriceEditViewModel>();
            if (viewModel == null)
            {
                return;
            }
            this.WindowService.Title = "销售报价-编辑";
            viewModel.Model.Id = this.SelectedModel.Id;
            viewModel.RefreshCallbackAsync = this.QueryAsync;
            this.WindowService.Show("SalesPriceEditView", viewModel);
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.CustomerName = string.Empty;
            this.ValidDate = null;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    if (this.SelectedModel != null)
                    {
                        await _salesPriceAppService.DeleteAsync(this.SelectedModel.Id);
                        await this.QueryAsync();
                    }
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
                SalesPricePagedRequestDto requestDto = new SalesPricePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.CustomerName = this.CustomerName;

                if (this.ValidDate != null)
                {
                    requestDto.ValidDate = DateTime.SpecifyKind(ValidDate.Value, DateTimeKind.Local);
                }

                var result = await _salesPriceAppService.GetPagedListAsync(requestDto);
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
