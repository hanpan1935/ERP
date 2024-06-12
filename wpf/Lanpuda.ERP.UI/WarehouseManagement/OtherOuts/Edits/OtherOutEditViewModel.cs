using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelectWithSalesPrice;
using Lanpuda.ERP.UI.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts.Edits
{
    public class OtherOutEditViewModel : EditViewModelBase<OtherOutEditModel>
    {
        private readonly IOtherOutAppService _otherOutAppService;
        private readonly IServiceProvider _serviceProvider;

        public Func<Task>? RefreshCallbackAsync { get; set; }

        public OtherOutEditViewModel(
            IOtherOutAppService otherOutAppService,
            IServiceProvider serviceProvider)
        {
            _otherOutAppService = otherOutAppService;
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
                    this.PageTitle = "其他出库-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _otherOutAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.Description = result.Description;
                    foreach (var detail in result.Details)
                    {
                        OtherOutDetailEditModel detailModel = new OtherOutDetailEditModel();
                        detailModel.Id = detail.Id;
                        detailModel.ProductId = detail.ProductId;
                        detailModel.LocationId = detail.LocationId;
                        detailModel.Batch = detail.Batch;
                        detailModel.Quantity = detail.Quantity;
             
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.WarehouseName = detail.WarehouseName;
                        detailModel.LocationName = detail.LocationName;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "其他出库-新建";
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
                this.WindowService.Title = "请选择产品";
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
                OtherOutCreateDto dto = new OtherOutCreateDto();
                dto.Description = Model.Description;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    OtherOutDetailCreateDto detailDto = new OtherOutDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.LocationId = detail.LocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _otherOutAppService.CreateAsync(dto);
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
                OtherOutUpdateDto dto = new OtherOutUpdateDto();
                dto.Description = Model.Description;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    OtherOutDetailUpdateDto detailDto = new OtherOutDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.Id = detail.Id;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _otherOutAppService.UpdateAsync((Guid)Model.Id, dto);
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
                OtherOutDetailEditModel detailEditModel = new OtherOutDetailEditModel();
                detailEditModel.LocationId = inventory.LocationId;
                detailEditModel.Batch = inventory.Batch;
                detailEditModel.ProductId = inventory.ProductId;
                detailEditModel.ProductName = inventory.ProductName;
                detailEditModel.ProductNumber = inventory.ProductNumber;
                detailEditModel.ProductSpec = inventory.ProductSpec;
                detailEditModel.ProductUnitName = inventory.ProductUnitName;
                detailEditModel.WarehouseName = inventory.WarehouseName;
                detailEditModel.LocationName = inventory.LocationName;
                this.Model.Details.Add(detailEditModel);
            }
        }
    }
}

