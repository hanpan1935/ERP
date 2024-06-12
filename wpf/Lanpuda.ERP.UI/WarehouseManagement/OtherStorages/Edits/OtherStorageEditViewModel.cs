using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelectWithSalesPrice;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelect;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages.Edits
{
    public class OtherStorageEditViewModel : EditViewModelBase<OtherStorageEditModel>
    {
        private readonly IOtherStorageAppService _otherStorageAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        private List<WarehouseDto> _warehouseList;


        public Func<Task>? RefreshCallbackAsync { get; set; }

        public OtherStorageEditViewModel(
            IOtherStorageAppService otherStorageAppService,
            IWarehouseAppService warehouseLookupAppService,
            IServiceProvider serviceProvider)
        {
            _otherStorageAppService = otherStorageAppService;
            _warehouseLookupAppService = warehouseLookupAppService;
            _serviceProvider = serviceProvider;
            _warehouseList = new List<WarehouseDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                _warehouseList = await _warehouseLookupAppService.GetAllAsync();

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "销售订单-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _otherStorageAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.Description = result.Description;
                    foreach (var detail in result.Details)
                    {
                        OtherStorageDetailEditModel detailModel = new OtherStorageDetailEditModel(_warehouseList);
                        detailModel.Id = detail.Id;
                        detailModel.ProductId = detail.ProductId;
                        detailModel.LocationId = detail.LocationId;
                        detailModel.Batch = detail.Batch;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.Price = detail.Price;
                        //
                        detailModel.SelectedWarehouse = detailModel.WarehouseSource.Where(m =>m.Id == detail.WarehouseId ).FirstOrDefault();
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.ProductSpec = detail.ProductSpec;

                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "销售订单-新建";
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
        public void ShowSelectProductWindow()
        {
            ProductMultipleSelectViewModel? viewModel = _serviceProvider.GetService<ProductMultipleSelectViewModel>();
            if (viewModel != null)
            {
                viewModel.SaveCallback = this.OnProductSelected;
                this.WindowService.Title = "请选择产品";
                this.WindowService.Show("ProductMultipleSelectView", viewModel);
            }
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedDetailRow != null)
            {
                this.Model.Details.Remove(Model.SelectedDetailRow);
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
        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                OtherStorageCreateDto dto = new OtherStorageCreateDto();
                dto.Description = Model.Description;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    OtherStorageDetailCreateDto detailDto = new OtherStorageDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Price = detail.Price;
                    dto.Details.Add(detailDto);
                }
                await _otherStorageAppService.CreateAsync(dto);
                CurrentWindowService.Close();
                if (RefreshCallbackAsync != null)
                {
                    await RefreshCallbackAsync();
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

        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                OtherStorageUpdateDto dto = new OtherStorageUpdateDto();
                dto.Description = Model.Description;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    OtherStorageDetailUpdateDto detailDto = new OtherStorageDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.Id = detail.Id;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Price = detail.Price;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _otherStorageAppService.UpdateAsync((Guid)Model.Id, dto);
                CurrentWindowService.Close();
                if (RefreshCallbackAsync != null)
                {
                    await RefreshCallbackAsync();
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

        private void OnProductSelected(ICollection<ProductDto> products)
        {
            foreach (var item in products)
            {
                OtherStorageDetailEditModel detailEditModel = new OtherStorageDetailEditModel(_warehouseList);
                detailEditModel.ProductId = item.Id;
                detailEditModel.ProductName = item.Name;
                detailEditModel.ProductNumber = item.Number;
                detailEditModel.ProductSpec = item.Spec;
                detailEditModel.ProductUnitName = item.ProductUnitName;
                this.Model.Details.Add(detailEditModel);
            }
        }
    }
}

