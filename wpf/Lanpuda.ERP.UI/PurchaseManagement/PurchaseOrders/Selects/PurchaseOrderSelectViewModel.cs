using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Selects
{
    public class PurchaseOrderSelectViewModel : PagedViewModelBase<PurchaseOrderDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        private readonly IServiceProvider _serviceProvider;
        private readonly IObjectMapper _objectMapper;
        private readonly ISupplierAppService _supplierLookupAppService;
        private readonly IPurchaseOrderAppService _purchaseOrderAppService;

        public Action<PurchaseOrderDto>? OnSelectedCallback { get; set; }


        public PurchaseOrderPagedRequestModel RequestModel
        {
            get { return GetProperty(() => RequestModel); }
            set { SetProperty(() => RequestModel, value); }
        }


        public PurchaseOrderSelectViewModel(
            IServiceProvider serviceProvider,
            IObjectMapper objectMapper,
            ISupplierAppService supplierLookupAppService,
            IPurchaseOrderAppService purchaseOrderAppService
            )
        {
            _serviceProvider = serviceProvider;
            _objectMapper = objectMapper;
            _supplierLookupAppService = supplierLookupAppService;
            _purchaseOrderAppService = purchaseOrderAppService;
            RequestModel = new PurchaseOrderPagedRequestModel();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var suppliers = await _supplierLookupAppService.GetAllAsync();
                this.RequestModel._dataList = suppliers;
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



        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.RequestModel.Number = String.Empty;
            RequestModel.SupplierId = null;
            RequestModel.RequiredDateStart = null;
            RequestModel.RequiredDateEnd = null;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.OnSelectedCallback != null && this.SelectedModel != null)
            {
                OnSelectedCallback(this.SelectedModel);
                if (CurrentWindowService != null)
                    CurrentWindowService.Close();
            }
        }




        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchaseOrderPagedRequestDto requestDto = new PurchaseOrderPagedRequestDto();
                requestDto.Number = RequestModel.Number;
                requestDto.SupplierId = RequestModel.SupplierId;
                requestDto.RequiredDateStart = RequestModel.RequiredDateStart;
                requestDto.RequiredDateEnd = RequestModel.RequiredDateEnd;

                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                var result = await _purchaseOrderAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    //PurchaseOrderPagedModel pagedModel = this._objectMapper.Map<PurchaseOrderDto, PurchaseOrderPagedModel>(item);
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
