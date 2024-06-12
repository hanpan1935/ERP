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
using DevExpress.Mvvm.ModuleInjection;
using Volo.Abp.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Edits;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Edits;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns
{
    public class PurchaseReturnPagedViewModel : PagedViewModelBase<PurchaseReturnDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchaseReturnAppService _purchaseReturnAppService;
        private readonly IObjectMapper _objectMapper;

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

        public string SupplierName
        {
            get { return GetProperty(() => SupplierName); }
            set { SetProperty(() => SupplierName, value); }
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


        public Dictionary<string, bool> IsSuccessfulSource { get; set; }
        #endregion

        public PurchaseReturnPagedViewModel(
            IServiceProvider serviceProvider,
            IPurchaseReturnAppService purchaseReturnAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _purchaseReturnAppService = purchaseReturnAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "采购退货";
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
            PurchaseReturnEditViewModel? purchaseReturnEditViewModel = _serviceProvider.GetService<PurchaseReturnEditViewModel>();
            if (purchaseReturnEditViewModel != null)
            {
                WindowService.Title = "采购退货-编辑";
                purchaseReturnEditViewModel.OnCloseWindowCallbackAsync = QueryAsync;
                purchaseReturnEditViewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("PurchaseReturnEditView", purchaseReturnEditViewModel);
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
            this.IsSuccessful = null;
            this.SupplierName = string.Empty;
            this.ProductName = string.Empty;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task StoragedAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                this.IsLoading = true;
                await _purchaseReturnAppService.OutedAsync(this.SelectedModel.Id);
                await this.QueryAsync();
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
            if (SelectedModel.Details.Count == 0)
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
                PurchaseReturnPagedRequestDto requestDto = new PurchaseReturnPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.ApplyNumber = this.ApplyNumber;
                requestDto.IsSuccessful = this.IsSuccessful;
                requestDto.SupplierName = this.SupplierName;
                requestDto.ProductName = this.ProductName;
                var result = await _purchaseReturnAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                PagedDatas.CanNotify = true;
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
