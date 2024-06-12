using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.ERP.WarehouseManagement.Inventories
{
    public class InventoryPagedViewModel : PagedViewModelBase<InventoryDto>
    {
        private readonly IInventoryAppService _inventoryAppService;
        private readonly IWarehouseAppService _warehouseAppService;

        #region 搜索
        public Guid? ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public string ProductSpec
        {
            get { return GetProperty(() => ProductSpec); }
            set { SetProperty(() => ProductSpec, value); }
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

        public LocationDto? SelectedLocation
        {
            get { return GetProperty(() => SelectedLocation); }
            set { SetProperty(() => SelectedLocation, value); }
        }


        public ObservableCollection<WarehouseDto> WarehouseSource { get; set; }
        #endregion

        public double TotalQuantity
        {
            get { return GetProperty(() => TotalQuantity); }
            set { SetProperty(() => TotalQuantity, value); }
        }

        public InventoryPagedViewModel(IInventoryAppService inventoryAppService, IWarehouseAppService warehouseAppServic)
        {
            _inventoryAppService = inventoryAppService;
            this.PageTitle = "库存查询";
            WarehouseSource = new ObservableCollection<WarehouseDto>();
            _warehouseAppService = warehouseAppServic;

        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                var warehouseList = await _warehouseAppService.GetAllAsync();
                WarehouseSource.Clear();
                foreach (var item in warehouseList)
                {
                    WarehouseSource.Add(item);
                }

                await this.QueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

       

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.ProductId = null;
            this.ProductName = string.Empty;
            this.ProductSpec = string.Empty;
            this.Batch = string.Empty;
            this.SelectedWarehouse = null;
            this.SelectedLocation = null;
            await QueryAsync();
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryPagedRequestDto requestDto = new InventoryPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                if (this.SelectedWarehouse != null)
                {
                    requestDto.WarehouseId = SelectedWarehouse.Id;
                    if (this.SelectedLocation != null)
                    {
                        requestDto.LocationId = SelectedLocation.Id;
                    }
                }
                requestDto.ProductName = this.ProductName;
                requestDto.ProductId = this.ProductId;
                requestDto.ProductSpec = this.ProductSpec;
                requestDto.Batch = this.Batch;
                var result = await _inventoryAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.TotalQuantity = result.TotalQuantity;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify = true;
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
