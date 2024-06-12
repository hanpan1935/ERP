using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.ActiveDirectory;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.BasicData.Products.Selects.SelectAll;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelect;

namespace Lanpuda.ERP.ProductionManagement.Boms.Edits
{
    public class BomEditViewModel : EditViewModelBase<BomEditModel>
    {
        private readonly IBomAppService _bomAppService;
        private readonly IProductLookupAppService _productLookupAppService;
        private readonly IServiceProvider _serviceProvider;

        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        public BomEditViewModel(
            IBomAppService bomAppService,
            IProductLookupAppService productAppService,
            IServiceProvider serviceProvider
            )
        {
            _bomAppService = bomAppService;
            _productLookupAppService = productAppService;
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
                    this.PageTitle = "生产BOM-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _bomAppService.GetAsync(id);
                    Model.ProductId = result.ProductId;
                    Model.ProductName = result.ProductName;
                    Model.ProductSpec = result.ProductSpec;
                    Model.ProductUnitName = result.ProductUnitName;
                    Model.Remark = result.Remark;
                    this.Model.Details.Clear();
                    foreach (var detail in result.Details)
                    {
                        BomDetailEditModel detailModel = new BomDetailEditModel();
                        detailModel.ProductId = detail.ProductId;
                        detailModel.ProductNumber= detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnitName= detail.ProductUnitName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.Remark = detail.Remark;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "生产BOM-新建";
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



        /// <summary>
        /// 选择主产品
        /// </summary>
        [Command]
        public void ShowSelectMainProductWindow()
        {
            if (this.WindowService != null)
            {
                ProductSelectAllViewModel? viewModel = _serviceProvider.GetService<ProductSelectAllViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = OnMainProductSelectd;
                    WindowService.Title = "选择要设置Bom的产品";
                    WindowService.Show("ProductSelectAllView", viewModel);
                }
            }
        }

        private void OnMainProductSelectd(ProductDto product)
        {
            Model.ProductId = product.Id;
            Model.ProductNumber = product.Number;
            Model.ProductName = product.Name;
            Model.ProductUnitName = product.ProductUnitName;
            Model.ProductSpec = product.Spec;
            Model.ProductSourceType = product.SourceType;
        }


        [Command]
        public void ShowAddProductWindow()
        {
            ProductMultipleSelectViewModel? viewModel = _serviceProvider.GetService<ProductMultipleSelectViewModel>();
            if (viewModel != null)
            {
                viewModel.SaveCallback = OnAddProduct;
                WindowService.Title = "选择Bom明细的产品";
                WindowService.Show("ProductMultipleSelectView", viewModel);
            }
        }

        private void OnAddProduct(ICollection<ProductDto> products)
        {
            foreach (var item in products)
            {
                BomDetailEditModel detailEditModel = new BomDetailEditModel();
                detailEditModel.ProductId = item.Id;
                detailEditModel.Quantity = 0;
                detailEditModel.ProductName = item.Name;
                detailEditModel.ProductNumber = item.Number;
                detailEditModel.ProductSpec = item.Spec;
                detailEditModel.ProductUnitName = item.ProductUnitName;
                detailEditModel.ProductSourceType= item.SourceType;
                this.Model.Details.Add(detailEditModel);
            }
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (this.Model.SelectedRow != null)
            {
                this.Model.Details.Remove(Model.SelectedRow);
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
            if (this.OnCloseWindowCallbackAsync != null)
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
            return true;
        }

        private async Task Create()
        {
            try
            {
                this.IsLoading = true;
                BomCreateDto dto = new BomCreateDto();
                dto.ProductId = Model.ProductId;
                dto.Remark = Model.Remark;

                for (int i = 0; i < Model.Details.Count; i++)
                {
                    BomDetailCreateDto detailDto = new BomDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Remark = detail.Remark;
                    dto.Details.Add(detailDto);
                }
                await _bomAppService.CreateAsync(dto);
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
                BomUpdateDto dto = new BomUpdateDto();
                dto.ProductId = Model.ProductId;
                dto.Remark = Model.Remark;

                for (int i = 0; i < Model.Details.Count; i++)
                {
                    BomDetailUpdateDto detailDto = new BomDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Remark = detail.Remark;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _bomAppService.UpdateAsync((Guid)Model.Id, dto);
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

