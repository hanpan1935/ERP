using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelect;
using Lanpuda.ERP.UI.WarehouseManagement.InventoryChecks.Edits;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Edits
{
    public class InventoryCheckEditViewModel : EditViewModelBase<InventoryCheckEditModel>
    {
        private readonly IInventoryCheckAppService _inventoryCheckAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        private readonly IInventoryAppService _inventoryAppService;


        public Func<Task>? RefreshCallbackAsync { get; set; }

        public InventoryCheckEditViewModel(
            IInventoryCheckAppService inventoryCheckAppService,
            IWarehouseAppService warehouseLookupAppService,
            IInventoryAppService inventoryAppService,
            IServiceProvider serviceProvider)
        {
            _inventoryCheckAppService = inventoryCheckAppService;
            _serviceProvider = serviceProvider;
            _warehouseLookupAppService = warehouseLookupAppService;
            _inventoryAppService = inventoryAppService;
            Model.OnWarehouseChangedAsync = GetInventoryListByWarehouseIdAsync;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouseList = await _warehouseLookupAppService.GetAllAsync();
                foreach (var item in warehouseList)
                {
                    Model.WarehouseSource.Add(item);
                }

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "库存盘点-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _inventoryCheckAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.CheckDate = result.CheckDate;
                    Model.Discription = result.Discription;
                    Model.WarehouseId = result.WarehouseId;
                    foreach (var detail in result.Details)
                    {
                        InventoryCheckDetailEditModel detailModel = new InventoryCheckDetailEditModel();
                        detailModel.Id = detail.Id;
                        detailModel.ProductId = detail.ProductId;
                        detailModel.LocationId = detail.LocationId;
                        detailModel.Batch = detail.Batch;
                        detailModel.InventoryQuantity = detail.InventoryQuantity;
                        detailModel.CheckType = detail.CheckType;
                        detailModel.CheckQuantity = detail.CheckQuantity;
                        if (detail.CheckType == InventoryCheckDetailType.None)
                        {
                            detailModel.RealQuantity = detail.InventoryQuantity;
                        }
                        else if (detail.CheckType == InventoryCheckDetailType.Add)
                        {
                            detailModel.RealQuantity = detail.InventoryQuantity + detail.CheckQuantity  ;
                        }
                        else if (detail.CheckType == InventoryCheckDetailType.Reduce)
                        {
                            detailModel.RealQuantity = detail.InventoryQuantity - detail.CheckQuantity;
                        }
                        //detailModel.ProductNumber = detail.ProductNumber;
                        //detailModel.ProductName = detail.ProductName;
                        //detailModel.ProductUnitName = detail.ProductUnitName;
                        //detailModel.ProductSpec = detail.ProductSpec;
                        //detailModel.WarehouseName = detail.WarehouseName;
                        //detailModel.LocationName = detail.LocationName;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "库存盘点-新建";
                    this.Model.Number = "系统自动生成";
                    this.Model.CheckDate = DateTime.Now;
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
        public void ShowAddProductWindow()
        {
            InventoryCheckEditAddProductViewModel? viewModel = _serviceProvider.GetService<InventoryCheckEditAddProductViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "库存盘点-添加产品";
                viewModel.SaveCallback = OnProductSelected;
                viewModel.WarehouseId = this.Model.WarehouseId;
                WindowService.Show("InventoryCheckEditAddProductView", viewModel);
            }
        }
      

        public bool CanShowAddProductWindow()
        {
            if (this.Model.WarehouseId == Guid.Empty)
            {
                return false;
            }
            return true;
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
                InventoryCheckCreateDto dto = new InventoryCheckCreateDto();
                dto.CheckDate = Model.CheckDate;
                dto.Discription = Model.Discription;
                dto.WarehouseId = Model.WarehouseId;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    InventoryCheckDetailCreateDto detailDto = new InventoryCheckDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.InventoryQuantity = detail.InventoryQuantity;
                    detailDto.CheckType = detail.CheckType;
                    detailDto.CheckQuantity = detail.CheckQuantity;
                    dto.Details.Add(detailDto);
                }
                await _inventoryCheckAppService.CreateAsync(dto);
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
                InventoryCheckUpdateDto dto = new InventoryCheckUpdateDto();
                dto.CheckDate = Model.CheckDate;
                dto.Discription = Model.Discription;
                dto.WarehouseId = Model.WarehouseId;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    InventoryCheckDetailUpdateDto detailDto = new InventoryCheckDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.Id = detail.Id;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.InventoryQuantity = detail.InventoryQuantity;
                    detailDto.CheckType = detail.CheckType;
                    detailDto.CheckQuantity = detail.CheckQuantity;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _inventoryCheckAppService.UpdateAsync((Guid)Model.Id, dto);
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

        private async Task GetInventoryListByWarehouseIdAsync(Guid warehouseId)
        {
            try
            {
                this.IsLoading = true;
                InventoryPagedRequestDto inventoryPagedRequestDto = new InventoryPagedRequestDto();
                inventoryPagedRequestDto.WarehouseId = this.Model.WarehouseId;
                var result = await _inventoryAppService.GetPagedListAsync(inventoryPagedRequestDto);
                this.Model.Details.CanNotify = false;
                this.Model.Details.Clear();
                foreach ( var item in result.Items)
                {
                    InventoryCheckDetailEditModel detailEditModel = new InventoryCheckDetailEditModel();
                    detailEditModel.ProductId = item.ProductId;
                    detailEditModel.LocationId = item.LocationId;
                    detailEditModel.LocationName = item.LocationName;
                    detailEditModel.ProductNumber = item.ProductNumber;
                    detailEditModel.ProductName = item.ProductName;
                    detailEditModel.ProductSpec = item.ProductSpec;
                    detailEditModel.ProductUnitName = item.ProductUnitName;
                    detailEditModel.Batch = item.Batch;
                    detailEditModel.InventoryQuantity = item.Quantity;
                    detailEditModel.RealQuantity = item.Quantity;
                    this.Model.Details.Add(detailEditModel);
                }
                this.Model.Details.CanNotify = true;
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


        private void OnProductSelected(InventoryCheckEditAddProductModel addProductModel)
        {
            InventoryCheckDetailEditModel detailEditModel = new InventoryCheckDetailEditModel();
            detailEditModel.ProductNumber = addProductModel.ProductNumber;
            detailEditModel.ProductName = addProductModel.ProductName;
            detailEditModel.ProductSpec = addProductModel.ProductSpec;
            detailEditModel.ProductUnitName = addProductModel.ProductUnitName;
            detailEditModel.LocationId = addProductModel.LocationId;
            detailEditModel.LocationName = addProductModel.LocationName;
            detailEditModel.InventoryQuantity = 0;

            this.Model.Details.Add(detailEditModel);
        }
       
    }
}

