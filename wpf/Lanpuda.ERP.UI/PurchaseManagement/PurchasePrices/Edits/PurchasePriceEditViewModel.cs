using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelect;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Lanpuda.ERP.UI.PurchaseManagement.PurchasePrices.Selects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Edits
{
    public class PurchasePriceEditViewModel : EditViewModelBase<PurchasePriceEditModel>
    {

        private readonly IPurchasePriceAppService _purchasePriceAppService;
        private readonly ISupplierAppService _supplierLookupAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? OnBackCallbackAsync { get; set; }

        public PurchasePriceEditViewModel(
            IPurchasePriceAppService purchasePriceAppService,
            ISupplierAppService supplierLookupAppService,
            IServiceProvider serviceProvider
            )
        {
            _purchasePriceAppService = purchasePriceAppService;
            _supplierLookupAppService = supplierLookupAppService;
            _serviceProvider = serviceProvider;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var suppliers = await _supplierLookupAppService.GetAllAsync();
                this.Model.SupplierSource = new ObservableCollection<SupplierDto>(suppliers);
                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "采购报价-编辑";
                    Guid id = (Guid)Model.Id;
                    var result = await _purchasePriceAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.SupplierId = result.SupplierId;
                    Model.AvgDeliveryDate = result.AvgDeliveryDate;
                    Model.QuotationDate = result.QuotationDate;
                    Model.Remark = result.Remark;
                    this.Model.Details.Clear();
                    foreach (var detail in result.Details)
                    {
                        PurchasePriceDetailEditModel detailModel = new PurchasePriceDetailEditModel();
                        detailModel.ProductId = detail.ProductId;
                        detailModel.ProductNumner = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.Price = detail.Price;
                        detailModel.TaxRate = detail.TaxRate;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "采购报价-新建";
                    this.Model.Number = "系统自动生成";
                    this.Model.QuotationDate = DateTime.Now;
                }
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

        [Command]
        public void ShowSelectProductWindow()
        {
            ProductMultipleSelectViewModel? viewModel = _serviceProvider.GetService<ProductMultipleSelectViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "产品选择";
                viewModel.SaveCallback = OnProductSelected;
                WindowService.Show("ProductMultipleSelectView", viewModel);
            }
        }

        private void OnProductSelected(ICollection<ProductDto> products)
        {

            foreach (var item in products)
            {
                var isExsits = this.Model.Details.Any(m => m.ProductId == item.Id);
                if (isExsits) { continue; }
                PurchasePriceDetailEditModel detail = new PurchasePriceDetailEditModel();
                detail.ProductId = item.Id;
                detail.ProductNumner = item.Number;
                detail.ProductName= item.Name;
                detail.ProductUnitName = item.ProductUnitName;
                detail.ProductSpec = item.Spec;
                detail.Price = 0;
                detail.TaxRate = 0;
                this.Model.Details.Add(detail);
            }
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow != null)
            {
                this.Model.Details.Remove(Model.SelectedRow);
            }
        }


        [Command]
        public void ShowSelectPurchasePriceWindow()
        {
            PurchasePriceSelectViewModel? viewModel = _serviceProvider.GetService<PurchasePriceSelectViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "请选择报价单";
                viewModel.OnSelectedCallback = OnPurchasePriceSelectCallback;
                WindowService.Show("PurchasePriceSelectView", viewModel);
            }
        }

        private void OnPurchasePriceSelectCallback(PurchasePriceDto purchasePrice)
        {
            foreach (var item in purchasePrice.Details)
            {
                PurchasePriceDetailEditModel editModel = new PurchasePriceDetailEditModel();
                editModel.ProductId = item.ProductId;
                editModel.ProductNumner = item.ProductNumber;
                editModel.ProductName = item.ProductName;
                editModel.ProductSpec = item.ProductSpec;
                editModel.ProductUnitName = item.ProductUnitName;
                editModel.Price = item.Price; 
                editModel.TaxRate = item.TaxRate;
                this.Model.Details.Add(editModel);
            }
        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null || Model.Id == Guid.Empty)
            {
                await Create();
            }
            else
            {
                await Update();
            }

            this.CurrentWindowService.Close();

            if (OnBackCallbackAsync != null)
            {
                await OnBackCallbackAsync();
            }
           
        }
      
        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            if (hasError == true) return !hasError;
            if (Model.Details.Count == 0)
            {
                return false;
            }
            foreach (var item in Model.Details)
            {
                if (item.HasErrors())
                {
                    return false;
                }
            }
            return true;
        }

        private async Task Create()
        {
            try
            {
                this.IsLoading = true;
                PurchasePriceCreateDto dto = new PurchasePriceCreateDto();
                dto.SupplierId = Model.SupplierId;
                dto.AvgDeliveryDate = Model.AvgDeliveryDate;
                dto.QuotationDate = Model.QuotationDate;
                dto.Remark = Model.Remark;

                for (int i = 0; i < Model.Details.Count; i++)
                {
                    PurchasePriceDetailCreateDto detailDto = new PurchasePriceDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Price = (detail.Price);
                    detailDto.TaxRate = ((detail.TaxRate));
                    dto.Details.Add(detailDto);
                }
                await _purchasePriceAppService.CreateAsync(dto);
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

        private async Task Update()
        {
            try
            {
                this.IsLoading = true;
                PurchasePriceUpdateDto dto = new PurchasePriceUpdateDto();
                dto.SupplierId = Model.SupplierId;
                dto.AvgDeliveryDate = Model.AvgDeliveryDate;
                dto.QuotationDate = Model.QuotationDate;
                dto.Remark = Model.Remark;

                for (int i = 0; i < Model.Details.Count; i++)
                {
                    PurchasePriceDetailUpdateDto detailDto = new PurchasePriceDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Price = (detail.Price);
                    detailDto.TaxRate = ((detail.TaxRate));
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _purchasePriceAppService.UpdateAsync((Guid)Model.Id, dto);
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
    }
}

