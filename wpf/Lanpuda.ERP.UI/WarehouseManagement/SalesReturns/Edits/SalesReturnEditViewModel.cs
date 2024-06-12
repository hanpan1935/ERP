using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Collections;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesReturns.Edits;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns.Edits
{
    public class SalesReturnEditViewModel : EditViewModelBase<SalesReturnEditModel>
    {
        private readonly ISalesReturnAppService _workOrderReturnAppService;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        private List<WarehouseDto> _warehouseList;

        public SalesReturnEditViewModel(
            ISalesReturnAppService workOrderReturnAppService,
            IWarehouseAppService warehouseLookupAppService,
            IServiceProvider serviceProvider
            )
        {
            _workOrderReturnAppService = workOrderReturnAppService;
            _warehouseLookupAppService = warehouseLookupAppService;
            _serviceProvider = serviceProvider;
            Model = new SalesReturnEditModel();
            _warehouseList = new List<WarehouseDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                _warehouseList = await _warehouseLookupAppService.GetAllAsync();
                this.Model.WarehouseSource = new ManualObservableCollection<WarehouseDto>(_warehouseList);
                var dto = await _workOrderReturnAppService.GetAsync(this.Model.Id);
                this.Model.Number = dto.Number;
                this.Model.Remark = dto.Remark;
                this.Model.ProductName = dto.ProductName;
                this.Model.ProductId = dto.ProductId;
                this.Model.Batch = dto.Batch;
                this.Model.ApplyQuantity = dto.ApplyQuantity;
                this.Model.ProductDefaultLocationId = dto.ProductDefaultLocationId;
                this.Model.ProductDefaultWarehouseId = dto.ProductDefaultWarehouseId;

                foreach (var item in dto.Details)
                {
                    SalesReturnDetailEditModel detail = new SalesReturnDetailEditModel(Model);
                    detail.Id = item.Id;
                    detail.SelectedWarehouse = this.Model.WarehouseSource.Where(m=>m.Id == item.WarehouseId).First();
                    detail.SelectedLocation = detail.SelectedWarehouse.Locations.Where(m=>m.Id == item.LocationId).First();
                    detail.Batch = item.Batch;
                    detail.Quantity = item.Quantity;
                    Model.Details.Add(detail);
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
        public void AddNewRow()
        {
            SalesReturnDetailEditModel detailEditModel = new SalesReturnDetailEditModel(this.Model);
            detailEditModel.Batch = this.Model.Batch;
            if (this.Model.ProductDefaultWarehouseId != null)
            {
                detailEditModel.SelectedWarehouse = Model.WarehouseSource.Where(m => m.Id == Model.ProductDefaultWarehouseId).First();
                detailEditModel.SelectedLocation= detailEditModel.SelectedWarehouse.Locations.Where(m=>m.Id== Model.ProductDefaultLocationId).First();
            }
            this.Model.Details.Add(detailEditModel);
            this.Model.NotifyTotalQuantityChanged();
        }


        [Command]
        public void AutoStorage()
        {
            if (this.Model.ProductDefaultWarehouseId == null || Model.ProductDefaultLocationId == null)
            {
                return;
            }
            this.Model.Details.Clear();
            SalesReturnDetailEditModel detailEditModel = new SalesReturnDetailEditModel(this.Model);
            detailEditModel.Batch = this.Model.Batch;
            detailEditModel.Quantity = this.Model.ApplyQuantity;
            detailEditModel.SelectedWarehouse = Model.WarehouseSource.Where(m => m.Id == Model.ProductDefaultWarehouseId).First();
            detailEditModel.SelectedLocation = detailEditModel.SelectedWarehouse.Locations.Where(m => m.Id == Model.ProductDefaultLocationId).First();
            this.Model.Details.Add(detailEditModel);
            this.Model.NotifyTotalQuantityChanged();
        }

        public bool CanAutoStorage()
        {
            if (this.Model.ProductDefaultWarehouseId == null || Model.ProductDefaultLocationId == null)
            {
                return false;
            }
            return true;
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (this.Model.SelectedRow != null)
            {
                this.Model.Details.Remove(this.Model.SelectedRow);
            }
        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesReturnUpdateDto dto = new SalesReturnUpdateDto();
                dto.Remark = this.Model.Remark;
                foreach (var detail in Model.Details)
                {
                    SalesReturnDetailUpdateDto detailDto = new SalesReturnDetailUpdateDto();
                    detailDto.Id = detail.Id;
                    if (detail.SelectedLocation != null)
                    {
                        detailDto.LocationId = detail.SelectedLocation.Id;
                    }
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _workOrderReturnAppService.UpdateAsync(this.Model.Id, dto);
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
