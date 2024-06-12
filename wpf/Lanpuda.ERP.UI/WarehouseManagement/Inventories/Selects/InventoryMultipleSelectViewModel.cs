using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lanpuda.ERP.UI.WarehouseManagement.Inventories.Selects
{
    public class InventoryMultipleSelectViewModel : PagedViewModelBase<InventoryDto>
    {
        private readonly IInventoryAppService _inventoryAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarehouseAppService _warehouseAppService;
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        public Action<ICollection<InventoryDto>>? OnSaveCallback;


        public InventoryDto? SelectedListSelectedRow
        {
            get { return GetProperty(() => SelectedListSelectedRow); }
            set { SetProperty(() => SelectedListSelectedRow, value); }
        }
        public ObservableCollection<InventoryDto> SelectedList { get;  set; }

        #region SearchItems

        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        public WarehouseDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value); }
        }

        public ObservableCollection<WarehouseDto> WarehouseSource { get; set; }

        public LocationDto? SelectedLocation
        {
            get { return GetProperty(() => SelectedLocation); }
            set { SetProperty(() => SelectedLocation, value); }
        }

        #endregion


        public InventoryMultipleSelectViewModel(
            IInventoryAppService inventoryAppService,
            IWarehouseAppService warehouseAppService,
            IServiceProvider serviceProvider)
        {
            _inventoryAppService = inventoryAppService;
            _serviceProvider = serviceProvider;
            _warehouseAppService = warehouseAppService;
            WarehouseSource = new ObservableCollection<WarehouseDto>();
            SelectedList = new ObservableCollection<InventoryDto>();
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouses = await _warehouseAppService.GetAllAsync();
                this.WarehouseSource.Clear();
                foreach (var item in warehouses)
                {
                    this.WarehouseSource.Add(item);
                }

                await this.QueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }

        }


        [Command]
        public void Selected()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            this.SelectedList.Add(SelectedModel);

        }

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Batch = string.Empty;
            this.SelectedLocation = null;
            this.SelectedWarehouse = null;
            this.ProductName = string.Empty;
            await this.QueryAsync();
        }


        [Command]
        public void DeleteSelectedListRow()
        {
            if (this.SelectedListSelectedRow != null)
            {
                SelectedList.Remove(SelectedListSelectedRow);
            }
        }

        [Command]
        public void Save()
        {
            if (OnSaveCallback != null)
            {
                OnSaveCallback(this.SelectedList);
                this.Close();
            }
        }

        [Command]
        public void Close()
        {
            this.CurrentWindowService.Close();
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryPagedRequestDto requestDto = new InventoryPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                //requestDto.pro = this.ProductName;

                if (this.SelectedWarehouse != null)
                {
                    requestDto.WarehouseId = SelectedWarehouse.Id;

                }

                if (this.SelectedLocation != null)
                {
                    requestDto.LocationId = this.SelectedLocation.Id;
                }

                requestDto.Batch = this.Batch;


                var result = await _inventoryAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                PagedDatas.CanNotify = true;
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
