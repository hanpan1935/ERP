using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelectWithPurchasePrice;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Edits
{
    public class PurchaseOrderEditViewModel : EditViewModelBase<PurchaseOrderEditModel>
    {
        private readonly IPurchaseOrderAppService _purchasePriceAppService;
        private readonly IProductLookupAppService _productLookupAppService;
        private readonly ISupplierAppService _supplierLookupAppService;
        private readonly IServiceProvider _serviceProvider;
       
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        public PurchaseOrderEditViewModel(
            IPurchaseOrderAppService purchasePriceAppService,
            IProductLookupAppService productLookupAppService,
            ISupplierAppService supplierLookupAppService,
            IServiceProvider serviceProvider
            )
        {
            _purchasePriceAppService = purchasePriceAppService;
            _productLookupAppService = productLookupAppService;
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
                Model.SupplierSource.Clear();
                foreach ( var supplier in suppliers ) 
                {
                    Model.SupplierSource.Add(supplier);
                }

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "采购订单-编辑";
                    Guid id = (Guid)Model.Id;
                    var result = await _purchasePriceAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.SupplierId = result.SupplierId;
                    Model.RequiredDate = result.RequiredDate;
                    Model.PromisedDate = result.PromisedDate;
                    Model.Contact = result.Contact;
                    Model.ContactTel = result.ContactTel;
                    Model.ShippingAddress = result.ShippingAddress;
                    Model.Remark = result.Remark;

                    this.Model.Details.Clear();
                    foreach (var detail in result.Details)
                    {
                        PurchaseOrderDetailEditModel detailModel = new PurchaseOrderDetailEditModel();
                        detailModel.ProductId = detail.ProductId;
                        detailModel.PromiseDate = detail.PromiseDate;
                        detailModel.Quantity = detail.Quantity ;
                        detailModel.Price = detail.Price;
                        detailModel.TaxRate = detail.TaxRate;
                        detailModel.Remark = detail.Remark;
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "采购订单-新建";
                    this.Model.Number = "系统自动生成";
                    this.Model.RequiredDate = DateTime.Now;
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
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow != null)
            {
                this.Model.Details.Remove(Model.SelectedRow);
            }
        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null || Model.Id == Guid.Empty)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }
            if (OnCloseWindowCallbackAsync != null)
            {
                await OnCloseWindowCallbackAsync();
            }
            this.Close();
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
     


        [Command]
        public void ShowSelectProductWindow()
        {
            MultipleSelectWithPurchasePriceViewModel? productSelectViewModel = _serviceProvider.GetService<MultipleSelectWithPurchasePriceViewModel>();
            if (productSelectViewModel != null)
            {
                WindowService.Title = "产品选择-" + this.Model.SupplierShortName;
                productSelectViewModel.SaveCallback = OnSelectedViewBacked;
                productSelectViewModel.SupplierId = this.Model.SupplierId;
                WindowService.Show("MultipleSelectWithPurchasePriceView", productSelectViewModel);
            }
        }

        private void OnSelectedViewBacked(ICollection<ProductWithPriceDto> products)
        {
            foreach (var item in products)
            {
                PurchaseOrderDetailEditModel detail = new PurchaseOrderDetailEditModel();
                detail.PromiseDate = DateTime.Now;
                detail.ProductId = item.Id;
                detail.ProductNumber = item.Number;
                detail.ProductName = item.Name;
                detail.ProductSpec = item.Spec;
                detail.ProductUnitName = item.ProductUnitName;
                detail.Quantity = 0;
                if (item.Price != null)
                {
                    detail.Price = (double)item.Price;
                }
                else
                {
                    detail.Price = 0;
                }
                if (item.TaxRate != null)
                {
                    detail.TaxRate = (double)item.TaxRate;
                }
                else
                {
                    detail.TaxRate = 0;
                }
                this.Model.Details.Add(detail);
            }
        }

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchaseOrderCreateDto dto = new PurchaseOrderCreateDto();
                dto.SupplierId = Model.SupplierId;
                dto.RequiredDate = Model.RequiredDate;
                dto.PromisedDate = Model.PromisedDate;
                dto.Contact = Model.Contact;
                dto.ContactTel = Model.ContactTel;
                dto.ShippingAddress = Model.ShippingAddress;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    PurchaseOrderDetailCreateDto detailDto = new PurchaseOrderDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.PromiseDate = detail.PromiseDate;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = (detail.Quantity);
                    detailDto.Price = (detail.Price);
                    detailDto.TaxRate = (detail.TaxRate);
                    detailDto.Remark = detail.Remark;
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

        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchaseOrderUpdateDto dto = new PurchaseOrderUpdateDto();
                dto.SupplierId = Model.SupplierId;
                dto.RequiredDate = Model.RequiredDate;
                dto.PromisedDate = Model.PromisedDate;
                dto.Contact = Model.Contact;
                dto.ContactTel = Model.ContactTel;
                dto.ShippingAddress = Model.ShippingAddress;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    PurchaseOrderDetailUpdateDto detailDto = new PurchaseOrderDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.PromiseDate = detail.PromiseDate;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = (detail.Quantity);
                    detailDto.Price = (detail.Price);
                    detailDto.TaxRate = (detail.TaxRate);
                    detailDto.Remark = detail.Remark;
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

