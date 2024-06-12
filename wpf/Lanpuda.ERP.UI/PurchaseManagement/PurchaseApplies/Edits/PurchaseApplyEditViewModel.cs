using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelectWithPurchasePrice;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelect;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using HandyControl.Controls;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Edits
{
    public class PurchaseApplyEditViewModel : EditViewModelBase<PurchaseApplyEditModel>
    {
        private readonly IPurchaseApplyAppService _purchasePriceAppService;
        private readonly IProductLookupAppService _productLookupAppService;
        private readonly ISupplierAppService _supplierLookupAppService;
        private readonly IServiceProvider _serviceProvider;

        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        public PurchaseApplyEditViewModel(
            IPurchaseApplyAppService purchasePriceAppService,
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
                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "采购订单-编辑";
                    Guid id = (Guid)Model.Id;
                    var result = await _purchasePriceAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.Remark = result.Remark;

                    this.Model.Details.Clear();
                    foreach (var detail in result.Details)
                    {
                        PurchaseApplyDetailEditModel detailModel = new PurchaseApplyDetailEditModel();
                        detailModel.ProductId = detail.ProductId;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.ArrivalDate = detail.ArrivalDate;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "采购订单-新建";
                    this.Model.Number = "系统自动生成";
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
            ProductMultipleSelectViewModel? productSelectViewModel = _serviceProvider.GetService<ProductMultipleSelectViewModel>();
            if (productSelectViewModel != null)
            {
                WindowService.Title = "产品选择";
                productSelectViewModel.SaveCallback = OnSelectedViewBacked;
                WindowService.Show("ProductMultipleSelectView", productSelectViewModel);
            }
        }

        private void OnSelectedViewBacked(ICollection<ProductDto> products)
        {
            foreach (var item in products)
            {
                var any = this.Model.Details.Any(m => m.ProductId == item.Id);
                if (any)
                {
                    Growl.Info( item.Name + "已经添加了");
                    continue;
                }
                PurchaseApplyDetailEditModel detail = new PurchaseApplyDetailEditModel();
                detail.ProductId = item.Id;
                detail.ProductName = item.Name;
                detail.ProductSpec = item.Spec;
                detail.ProductUnitName = item.ProductUnitName;
                detail.Quantity = 0;
                this.Model.Details.Add(detail);
            }
        }

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchaseApplyCreateDto dto = new PurchaseApplyCreateDto();
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    PurchaseApplyDetailCreateDto detailDto = new PurchaseApplyDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = (detail.Quantity);
                    detailDto.ArrivalDate = (detail.ArrivalDate);
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
                PurchaseApplyUpdateDto dto = new PurchaseApplyUpdateDto();
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    PurchaseApplyDetailUpdateDto detailDto = new PurchaseApplyDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = (detail.Quantity);
                    detailDto.ArrivalDate = (detail.ArrivalDate);
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

