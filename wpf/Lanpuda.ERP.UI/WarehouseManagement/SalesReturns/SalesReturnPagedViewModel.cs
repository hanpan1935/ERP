using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesReturns.Edits;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Edits;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.Client.Common;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns
{
    public class SalesReturnPagedViewModel : PagedViewModelBase<SalesReturnDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISalesReturnAppService _salesReturnAppService;

        #region 搜索

        
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

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public string CustomerName
        {
            get { return GetProperty(() => CustomerName); }
            set { SetProperty(() => CustomerName, value); }
        }


        public SalesReturnReason? Reason
        {
            get { return GetProperty(() => Reason); }
            set { SetProperty(() => Reason, value); }
        }

        public bool? IsSuccessful
        {
            get { return GetProperty(() => IsSuccessful); }
            set { SetProperty(() => IsSuccessful, value); }
        }

        public Dictionary<string, SalesReturnReason> SalesReturnReasonSource { get; set; }
        public Dictionary<string,bool> IsSuccessfulSource { get; set; }


        #endregion
        public SalesReturnPagedViewModel(IServiceProvider serviceProvider, ISalesReturnAppService salesReturnAppService)
        {
            _serviceProvider = serviceProvider;
            _salesReturnAppService = salesReturnAppService;
            this.PageTitle = "销售退货";
            IsSuccessfulSource = ItemsSoureUtils.GetBoolDictionary();
            SalesReturnReasonSource = EnumUtils.EnumToDictionary<SalesReturnReason>();
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
            SalesReturnEditViewModel? viewModel = _serviceProvider.GetService<SalesReturnEditViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "销售退货-编辑";
                viewModel.OnCloseWindowCallbackAsync = QueryAsync;
                viewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("SalesReturnEditView", viewModel);
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
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.ApplyNumber = string.Empty;
            this.CustomerName = string.Empty;
            this.Reason = null;
            this.IsSuccessful = null;
            this.ProductName = string.Empty;
            await QueryAsync();
        }


        [AsyncCommand]
        public async Task StoragedAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                this.IsLoading = true;
                await _salesReturnAppService.StoragedAsync(this.SelectedModel.Id);
                await this.QueryAsync();
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading= false;
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
                SalesReturnPagedRequestDto requestDto = new SalesReturnPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.ApplyNumber= this.ApplyNumber;
                requestDto.CustomerName = this.CustomerName;
                requestDto.Reason = this.Reason;
                requestDto.IsSuccessful = this.IsSuccessful;
                requestDto.ProductName = this.ProductName;
                var result = await _salesReturnAppService.GetPagedListAsync(requestDto);
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
