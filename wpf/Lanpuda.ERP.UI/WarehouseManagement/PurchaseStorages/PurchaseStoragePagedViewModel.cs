using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages
{
    public class PurchaseStoragePagedViewModel : PagedViewModelBase<PurchaseStorageDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchaseStorageAppService _purchaseStorageAppService;
        private readonly IObjectMapper _objectMapper;


        #region 搜索

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string ArrivalNoticeNumber
        {
            get { return GetProperty(() => ArrivalNoticeNumber); }
            set { SetProperty(() => ArrivalNoticeNumber, value); }
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


        public Dictionary<string,bool> IsSuccessfulSource { get; set; }

        #endregion


        public PurchaseStoragePagedViewModel(
            IServiceProvider serviceProvider,
            IPurchaseStorageAppService purchaseReturnApplyAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _purchaseStorageAppService = purchaseReturnApplyAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "采购入库";
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
            PurchaseStorageEditViewModel? purchaseStorageEditViewModel = _serviceProvider.GetService<PurchaseStorageEditViewModel>();
            if (purchaseStorageEditViewModel != null)
            {
                WindowService.Title = "采购入库-编辑";
                purchaseStorageEditViewModel.OnCloseWindowCallbackAsync = QueryAsync;
                purchaseStorageEditViewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("PurchaseStorageEditView", purchaseStorageEditViewModel);
            }
        }


        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }

            if (this.SelectedModel.IsSuccessful != false)
            {
                return false;
            }
            return true;
        }

  

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.ArrivalNoticeNumber= String.Empty;
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
                await _purchaseStorageAppService.StoragedAsync(this.SelectedModel.Id);
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
                PurchaseStoragePagedRequestDto requestDto = new PurchaseStoragePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.ArrivalNoticeNumber= this.ArrivalNoticeNumber;
                requestDto.IsSuccessful = this.IsSuccessful;
                var result = await _purchaseStorageAppService.GetPagedListAsync(requestDto);
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
