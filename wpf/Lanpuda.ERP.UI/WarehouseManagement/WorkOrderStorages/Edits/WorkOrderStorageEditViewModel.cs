using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Collections;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Edits
{
    public class WorkOrderStorageEditViewModel : EditViewModelBase<WorkOrderStorageEditModel>
    {
        private readonly IWorkOrderStorageAppService _workOrderStorageAppService;
        private readonly IWarehouseAppService _warehouseAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        private List<WarehouseDto> _warehouseList;

        public WorkOrderStorageEditViewModel(
            IWorkOrderStorageAppService workOrderStorageAppService,
            IWarehouseAppService warehouseAppService,
            IServiceProvider serviceProvider
            )
        {
            _workOrderStorageAppService = workOrderStorageAppService;
            _warehouseAppService = warehouseAppService;
            _serviceProvider = serviceProvider;
            _warehouseList = new List<WarehouseDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                _warehouseList = await _warehouseAppService.GetAllAsync();

                var dto = await _workOrderStorageAppService.GetAsync(this.Model.Id);
                Model.Number = dto.Number;
                Model.ApplyNumber = dto.ApplyNumber;
                Model.WorkOrderNumber = dto.WorkOrderNumber;
                Model.Remark = dto.Remark;
                Model.ProductId = dto.ProductId;
                Model.Quantity = dto.Quantity;
                Model.ProductName = dto.ProductName;
                Model.ProductDefaultLocationId = dto.ProductDefaultLocationId;
                Model.ProductDefaultWarehouseId = dto.ProductDefaultWarehouseId;
                Model.WarehouseSource = new ManualObservableCollection<WarehouseDto>(_warehouseList);

                foreach (var item in dto.Details)
                {
                    WorkOrderStorageDetailEditModel detailEditModel = new WorkOrderStorageDetailEditModel(Model);
                    detailEditModel.Id = item.Id;
                    detailEditModel.Quantity = item.Quantity;
                    detailEditModel.Batch = Model.WorkOrderNumber;
                    detailEditModel.SelectedWarehouse = Model.WarehouseSource.Where(m => m.Id == item.WarehouseId).FirstOrDefault();
                    if (detailEditModel.SelectedWarehouse != null)
                    {
                        detailEditModel.SelectedLocation = detailEditModel.SelectedWarehouse.Locations.Where(m => m.Id == item.LocationId).FirstOrDefault();
                    }
                    Model.Details.Add(detailEditModel);
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
        public void InsertRowBelow(Guid id)
        {
            WorkOrderStorageDetailEditModel detailEditModel = new WorkOrderStorageDetailEditModel(this.Model);
            detailEditModel.Batch = Model.WorkOrderNumber;
            Model.Details.Add(detailEditModel);
        }


        [Command]
        public void DeleteSelectedRow()
        {
           
            if (Model.SelectedRow != null)
            {
                Model.Details.Remove(Model.SelectedRow);
            }
        }

        [AsyncCommand]
        public async Task SaveAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderStorageUpdateDto updateDto = new WorkOrderStorageUpdateDto();
                updateDto.Remark = this.Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    var detail = Model.Details[i];
                    WorkOrderStorageDetailUpdateDto detailDto = new WorkOrderStorageDetailUpdateDto();
                    detailDto.Id = detail.Id;
                    if (detail.SelectedLocation != null)
                    {
                        detailDto.LocationId = detail.SelectedLocation.Id;
                    }
                    detailDto.Quantity = (double)detail.Quantity;
                    updateDto.Details.Add(detailDto);
                }
                await _workOrderStorageAppService.UpdateAsync(this.Model.Id, updateDto);
                if (this.OnCloseWindowCallbackAsync != null)
                {
                    await OnCloseWindowCallbackAsync();
                }
                this.Close();
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

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            if (hasError == true) return !hasError;
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
        public void AutoStorage()
        {
            if (this.Model.ProductDefaultLocationId == null || this.Model.ProductDefaultWarehouseId == null)
            {
                return;
            }
            WorkOrderStorageDetailEditModel detailEditModel = new WorkOrderStorageDetailEditModel(this.Model);
            detailEditModel.SelectedWarehouse = this.Model.WarehouseSource.Where(m=>m.Id == Model.ProductDefaultWarehouseId).First();
            detailEditModel.SelectedLocation = detailEditModel.SelectedWarehouse.Locations.Where(m=>m.Id == Model.ProductDefaultLocationId).First();
            detailEditModel.Batch = Model.ApplyNumber;
            detailEditModel.Quantity = Model.Quantity;
            this.Model.Details.Add(detailEditModel);
        }
        public bool CanAutoStorage()
        {
            if (this.Model.ProductDefaultLocationId == null || this.Model.ProductDefaultWarehouseId == null)
            {
                return false;
            }
            return true;
        }
    }
}
