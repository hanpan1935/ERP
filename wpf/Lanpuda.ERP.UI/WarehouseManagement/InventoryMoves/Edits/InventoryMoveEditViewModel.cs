using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelectWithSalesPrice;
using Lanpuda.ERP.UI.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;
using Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Edits;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves.Edits
{
    public class InventoryMoveEditViewModel : EditViewModelBase<InventoryMoveEditModel>
    {
        private readonly IInventoryMoveAppService _inventoryMoveAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        private List<WarehouseDto> _warehouseList;

        public Func<Task>? RefreshCallbackAsync { get; set; }

        public InventoryMoveEditViewModel(
            IInventoryMoveAppService inventoryMoveAppService,
            IWarehouseAppService warehouseLookupAppService,
            IServiceProvider serviceProvider)
        {
            _inventoryMoveAppService = inventoryMoveAppService;
            _serviceProvider = serviceProvider;
            _warehouseLookupAppService = warehouseLookupAppService;
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
                    this.PageTitle = "库存调拨-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _inventoryMoveAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.Reason = result.Reason;
                    Model.Remark = result.Remark;
                    foreach (var detail in result.Details)
                    {
                        InventoryMoveDetailEditModel detailModel = new InventoryMoveDetailEditModel(_warehouseList);
                        detailModel.Id = detail.Id;
                        detailModel.ProductId = detail.ProductId;
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.OutWarehouseName = detail.OutWarehouseName;
                        detailModel.OutLocationName = detail.OutLocationName;
                        detailModel.OutLocationId = detail.OutLocationId;
                        detailModel.Batch = detail.Batch;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.SelectedInWarehouse = detailModel.WarehouseSource.First(m=>m.Id == detail.InWarehouseId);
                        detailModel.InLocationId = detail.InLocationId;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "库存调拨-新建";
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
        public void ShowSelectInventoryWindow()
        {
            InventoryMultipleSelectViewModel? viewModel = _serviceProvider.GetService<InventoryMultipleSelectViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSaveCallback = this.OnInventorySelected;
                this.WindowService.Title = "请选择库存";
                this.WindowService.Show("InventoryMultipleSelectView", viewModel);
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
                InventoryMoveCreateDto dto = new InventoryMoveCreateDto();
                dto.Reason = Model.Reason;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    InventoryMoveDetailCreateDto detailDto = new InventoryMoveDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.OutLocationId = detail.OutLocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.InLocationId = detail.InLocationId;
                    dto.Details.Add(detailDto);
                }
                await _inventoryMoveAppService.CreateAsync(dto);
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
                InventoryMoveUpdateDto dto = new InventoryMoveUpdateDto();
                dto.Reason = Model.Reason;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    InventoryMoveDetailUpdateDto detailDto = new InventoryMoveDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.Id = detail.Id;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.OutLocationId = detail.OutLocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.InLocationId = detail.InLocationId;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _inventoryMoveAppService.UpdateAsync((Guid)Model.Id, dto);
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

        private void OnInventorySelected(ICollection<InventoryDto> inventoryList)
        {
            foreach (var inventory in inventoryList)
            {
                var any = Model.Details.Any(m=>m.OutLocationId == inventory.LocationId && m.ProductId == inventory.ProductId && m.Batch == inventory.Batch);
                if (any)
                {
                    continue;
                }
                InventoryMoveDetailEditModel detailEditModel = new InventoryMoveDetailEditModel(_warehouseList);
                detailEditModel.OutLocationId = inventory.LocationId;
                detailEditModel.Batch = inventory.Batch;
                detailEditModel.ProductId = inventory.ProductId;
                detailEditModel.ProductName = inventory.ProductName;
                detailEditModel.ProductNumber = inventory.ProductNumber;
                detailEditModel.ProductSpec = inventory.ProductSpec;
                detailEditModel.ProductUnitName = inventory.ProductUnitName;
                detailEditModel.OutWarehouseName = inventory.WarehouseName;
                detailEditModel.OutLocationName = inventory.LocationName;
                this.Model.Details.Add(detailEditModel);
            }
            
        }
    }
}

