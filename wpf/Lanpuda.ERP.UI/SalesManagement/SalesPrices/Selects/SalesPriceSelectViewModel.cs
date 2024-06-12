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

namespace Lanpuda.ERP.UI.SalesManagement.SalesPrices.Selects
{
    public class SalesPriceSelectViewModel : PagedViewModelBase<SalesPriceDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly ISalesPriceAppService _salesPriceAppService;

        public Action<SalesPriceDto>? OnSelectCallback;

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

        public SalesPriceSelectViewModel(IServiceProvider serviceProvider, ISalesPriceAppService salesPriceAppService)
        {
            _serviceProvider = serviceProvider;
            _salesPriceAppService = salesPriceAppService;
            this.PageTitle = "销售报价-选择";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.CustomerName = string.Empty;
            this.ValidDate = null;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            if (this.OnSelectCallback != null)
            {
                OnSelectCallback(this.SelectedModel);
                this.CurrentWindowService.Close();
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
