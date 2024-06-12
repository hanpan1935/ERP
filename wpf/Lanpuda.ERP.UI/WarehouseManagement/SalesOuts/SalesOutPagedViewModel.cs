using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Edits;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using Microsoft.Extensions.DependencyInjection;
using IdentityModel.Client;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts
{
    public class SalesOutPagedViewModel : PagedViewModelBase<SalesOutDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISalesOutAppService _salesOutAppService;

        #region 搜索
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string ShipmentApplyNumber
        {
            get { return GetProperty(() => ShipmentApplyNumber); }
            set { SetProperty(() => ShipmentApplyNumber, value); }
        }

        public string CustomerName
        {
            get { return GetProperty(() => CustomerName); }
            set { SetProperty(() => CustomerName, value); }
        }

        public bool? IsSuccessful
        {
            get { return GetProperty(() => IsSuccessful); }
            set { SetProperty(() => IsSuccessful, value); }
        }

        public Dictionary<string,bool> IsSuccessfulSource { get; set; }
        #endregion




        public SalesOutPagedViewModel(IServiceProvider serviceProvider, ISalesOutAppService salesOutAppService)
        {
            _serviceProvider = serviceProvider;
            _salesOutAppService = salesOutAppService;
            this.PageTitle = "销售出库";
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
            SalesOutEditViewModel? viewModel = _serviceProvider.GetService<SalesOutEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Model.Id = this.SelectedModel.Id;
                viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                WindowService.Title = "销售出库-编辑";
                WindowService.Show("SalesOutEditView", viewModel);
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsSuccessful == true)
            {
                return false;
            }
            return true;    
        }


        [AsyncCommand]
        public async Task OutedAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                this.IsLoading = true;
                await _salesOutAppService.OutedAsync(this.SelectedModel.Id);
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


        public bool CanOutedAsync()
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


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.ShipmentApplyNumber = string.Empty;
            this.CustomerName = string.Empty;
            this.IsSuccessful = null;
            await QueryAsync();
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesOutPagedRequestDto requestDto = new SalesOutPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.CustomerName = this.CustomerName;
                requestDto.IsSuccessful = this.IsSuccessful;
                requestDto.ShipmentApplyNumber = this.ShipmentApplyNumber;
                requestDto.Number = this.Number;
                var result = await _salesOutAppService.GetPagedListAsync(requestDto);
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
