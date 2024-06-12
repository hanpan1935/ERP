using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Collections;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Edits
{
    public class WorkOrderReturnEditViewModel : EditViewModelBase<WorkOrderReturnEditModel>
    {
        private readonly IWorkOrderReturnAppService _workOrderReturnAppService;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        private List<WarehouseDto> _warehouseList;

        public WorkOrderReturnEditViewModel(
            IWorkOrderReturnAppService workOrderReturnAppService,
            IWarehouseAppService warehouseLookupAppService,
            IServiceProvider serviceProvider
            )
        {
            _workOrderReturnAppService = workOrderReturnAppService;
            _warehouseLookupAppService = warehouseLookupAppService;
            _serviceProvider = serviceProvider;
            Model = new WorkOrderReturnEditModel();
            _warehouseList = new List<WarehouseDto>();
        }
     

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                _warehouseList = await _warehouseLookupAppService.GetAllAsync();

                var dto = await _workOrderReturnAppService.GetAsync(this.Model.Id);
                //this.Model.WorkOrderId = dto.WorkOrderId;
                this.Model.Number = dto.Number;
                this.Model.Remark = dto.Remark;
                this.Model.Id = dto.Id;
                this.Model.ProductId = dto.ProductId;
                this.Model.ProductDefaultLocationId = dto.ProductDefaultLocationId;
                this.Model.ProductDefaultWarehouseId = dto.ProductDefaultWarehouseId;
                Model.ProductName = dto.ProductName;
                Model.ProductSpec = dto.ProductSpec;
                Model.ProductUnitName = dto.ProductUnitName;
                Model.ApplyQuantity = dto.ApplyQuantity;
                Model.Batch = dto.Batch;
                Model.WarehouseSource = new ManualObservableCollection<WarehouseDto>(_warehouseList);
                foreach (var item in dto.Details)
                {
                    WorkOrderReturnDetailEditModel detailEditModel = new WorkOrderReturnDetailEditModel(Model);
                    detailEditModel.Id = item.Id;
                    detailEditModel.Batch = Model.Batch;
                    detailEditModel.Quantity = item.Quantity;
                    detailEditModel.SelectedWarehouse = Model.WarehouseSource.Where(m => m.Id == item.WarehouseId).First();
                    detailEditModel.SelectedLocation = detailEditModel.SelectedWarehouse.Locations.Where(m => m.Id == item.LocationId).First();
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
        public void InsertRowBelow()
        {
            WorkOrderReturnDetailEditModel detailEditModel = new WorkOrderReturnDetailEditModel(Model);
            detailEditModel.Quantity = Model.ApplyQuantity;
            detailEditModel.Batch = Model.Batch;
            Model.Details.Add(detailEditModel);
            Model.NotifyTotalQuantityChanged();
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow != null)
            {
                Model.Details.Remove(Model.SelectedRow);
                Model.NotifyTotalQuantityChanged();
            }
        }

        [Command]
        public void AutoStorage()
        {
            if (this.Model.ProductDefaultWarehouseId != null && this.Model.ProductDefaultLocationId != null)
            {
                this.Model.Details.Clear();
                WorkOrderReturnDetailEditModel detailEditModel = new WorkOrderReturnDetailEditModel(Model);
                detailEditModel.Quantity = Model.ApplyQuantity;
                detailEditModel.Batch = Model.Batch;
                detailEditModel.SelectedWarehouse = Model.WarehouseSource.Where(m=>m.Id == Model.ProductDefaultWarehouseId).First();
                detailEditModel.SelectedLocation = detailEditModel.SelectedWarehouse.Locations.Where(m=>m.Id == Model.ProductDefaultLocationId).First();
                Model.Details.Add(detailEditModel);
                Model.NotifyTotalQuantityChanged();
            }
        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderReturnUpdateDto dto = new WorkOrderReturnUpdateDto();
                dto.Remark = this.Model.Remark;

                foreach (var detail in Model.Details)
                {
                    WorkOrderReturnDetailUpdateDto detailDto = new WorkOrderReturnDetailUpdateDto();
                    detailDto.Id = detail.Id;
                    if (detail.SelectedLocation != null)
                    {
                        detailDto.LocationId = detail.SelectedLocation.Id;
                    }

                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }

                await _workOrderReturnAppService.UpdateAsync(this.Model.Id,dto);
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

        public bool CanSave()
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



    }
}
