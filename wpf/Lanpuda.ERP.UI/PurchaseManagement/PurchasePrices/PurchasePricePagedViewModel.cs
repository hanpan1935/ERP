using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Lanpuda.Client.Mvvm;
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
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices
{
    public class PurchasePricePagedViewModel : PagedViewModelBase<PurchasePriceDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchasePriceAppService _purchasePriceAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly ISupplierAppService _supplierLookupAppService;
       
    

        public PurchasePricePagedRequestModel RequestModel
        {
            get { return GetProperty(() => RequestModel); }
            set { SetProperty(() => RequestModel, value); }
        }

        public PurchasePricePagedViewModel(
                IServiceProvider serviceProvider, 
                IPurchasePriceAppService purchasePriceAppService,
                IObjectMapper objectMapper,
                ISupplierAppService supplierLookupAppService
                )
        {
            _serviceProvider = serviceProvider;
            _purchasePriceAppService = purchasePriceAppService;
            _supplierLookupAppService = supplierLookupAppService;
            this.PageTitle = "采购报价";
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
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }

            await this.QueryAsync();
        }

        [Command]
        public void Create()
        {
            //ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainContentRegion, ERPUIModuleKeys.PurchasePrice_Edit);
            if (this.WindowService != null)
            {
                PurchasePriceEditViewModel? purchasePriceEditViewModel = _serviceProvider.GetService<PurchasePriceEditViewModel>();
                if (purchasePriceEditViewModel != null)
                {
                    WindowService.Title = "采购报价-新建";
                    purchasePriceEditViewModel.OnBackCallbackAsync = QueryAsync;
                    WindowService.Show("PurchasePriceEditView", purchasePriceEditViewModel);
                }
            }
        }

        [Command]
        public void Update()
        {
            if (this.WindowService != null)
            {
                PurchasePriceEditViewModel? purchasePriceEditViewModel = _serviceProvider.GetService<PurchasePriceEditViewModel>();
                if (purchasePriceEditViewModel != null && SelectedModel != null)
                {
                    WindowService.Title = "采购报价-编辑";
                    purchasePriceEditViewModel.OnBackCallbackAsync = QueryAsync;
                    purchasePriceEditViewModel.Model.Id = this.SelectedModel.Id;
                    WindowService.Show("PurchasePriceEditView", purchasePriceEditViewModel);
                }
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            return true;
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

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null) return;
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _purchasePriceAppService.DeleteAsync(this.SelectedModel.Id);
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

        public bool CanDeleteAsync()
        {
            if (this.SelectedModel == null)
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
                PurchasePricePagedRequestDto requestDto = new PurchasePricePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount; 
                requestDto.SupplierId = RequestModel.SupplierId;
                requestDto.Number = RequestModel.Number;
                requestDto.QuotationDateStart = RequestModel.QuotationDateStart;
                requestDto.QuotationDateEnd = RequestModel.QuotationDateEnd;
                var result = await _purchasePriceAppService.GetPagedListAsync(requestDto);
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
