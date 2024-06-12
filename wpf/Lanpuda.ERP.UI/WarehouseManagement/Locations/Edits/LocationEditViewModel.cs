using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.Locations.Edits
{
    public class LocationEditViewModel : EditViewModelBase<LocationEditModel>
    {
        private readonly ILocationAppService _locationAppService;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        public Func<Task>? Refresh { get; set; }
       
        public ObservableCollection<WarehouseDto> WarehouseSource { get; set; }

        public LocationEditViewModel(
            ILocationAppService locationAppService,
            IWarehouseAppService warehouseLookupAppService
            )
        {
            _locationAppService = locationAppService;
            _warehouseLookupAppService = warehouseLookupAppService;
            WarehouseSource = new ObservableCollection<WarehouseDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouseList = await _warehouseLookupAppService.GetAllAsync();
                WarehouseSource.Clear();
                foreach (var item in warehouseList)
                {
                    this.WarehouseSource.Add(item);
                }
               
                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _locationAppService.GetAsync(id);
                    Model.WarehouseId = result.WarehouseId;
                    this.Model.Name = result.Name;
                    Model.Number = result.Number;
                    Model.Remark = result.Remark;
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



        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null)
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
                LocationCreateDto dto = new LocationCreateDto();
                dto.WarehouseId = Model.WarehouseId;
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                await _locationAppService.CreateAsync(dto);
                if (Refresh != null)
                {
                    await Refresh();
                    this.CurrentWindowService.Close();
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
                LocationUpdateDto dto = new LocationUpdateDto();
                dto.WarehouseId = Model.WarehouseId;
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _locationAppService.UpdateAsync((Guid)this.Model.Id, dto);
                if (Refresh != null)
                {
                    await Refresh();
                    this.CurrentWindowService.Close();
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
    }
}
