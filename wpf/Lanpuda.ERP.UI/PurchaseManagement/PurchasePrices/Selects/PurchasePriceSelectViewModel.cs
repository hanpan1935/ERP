using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices.Edits;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.Http.Modeling;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.UI.PurchaseManagement.PurchasePrices.Selects
{
    public class PurchasePriceSelectViewModel : PagedViewModelBase<PurchasePriceDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchasePriceAppService _purchasePriceAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly ISupplierAppService _supplierLookupAppService;
        public  Action<PurchasePriceDto>? OnSelectedCallback { get; set; }
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        public PurchasePricePagedRequestModel RequestModel
        {
            get { return GetProperty(() => RequestModel); }
            set { SetProperty(() => RequestModel, value); }
        }

        public PurchasePriceSelectViewModel(
                IServiceProvider serviceProvider,
                IPurchasePriceAppService purchasePriceAppService,
                IObjectMapper objectMapper,
                ISupplierAppService supplierLookupAppService
                )
        {
            _serviceProvider = serviceProvider;
            _purchasePriceAppService = purchasePriceAppService;
            _supplierLookupAppService = supplierLookupAppService;
            this.PageTitle = "采购报价-选择";
            _objectMapper = objectMapper;
            RequestModel = new PurchasePricePagedRequestModel();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {

            try
            {
                this.IsLoading = true;
                var supplierList = await _supplierLookupAppService.GetAllAsync();
                this.RequestModel.SupplierSource = new ObservableCollection<SupplierDto>(supplierList);
                await this.QueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally { this.IsLoading = false; }
        }
        


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.RequestModel.Number = String.Empty;
            this.RequestModel.SupplierId = null;
            this.RequestModel.QuotationDateStart = null;
            RequestModel.QuotationDateEnd = null;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            if (this.OnSelectedCallback != null)
            {
                OnSelectedCallback(this.SelectedModel);
                this.CurrentWindowService.Close();
            }
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchasePricePagedRequestDto requestDto = new PurchasePricePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.SupplierId = RequestModel.SupplierId;
                requestDto.Number = RequestModel.Number;
                requestDto.QuotationDateStart = RequestModel.QuotationDateStart;
                requestDto.QuotationDateEnd = RequestModel.QuotationDateEnd;
                var result = await _purchasePriceAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.SelectedModel = this.PagedDatas.FirstOrDefault();
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
