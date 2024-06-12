using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.ActiveDirectory;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using System.Windows;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using AutoUpdaterDotNET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Edits
{
    public class PurchaseStorageEditViewModel : EditViewModelBase<PurchaseStorageEditModel>
    {
        private readonly IPurchaseStorageAppService _purchaseStorageAppService;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        private readonly ILocationAppService _locationAppService;
       
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }
        

      

        public PurchaseStorageEditViewModel(
            IPurchaseStorageAppService purchaseStorageAppService,
            IProductLookupAppService productAppService,
            ISupplierAppService supplierAppService,
            IWarehouseAppService warehouseLookupAppService,
            ILocationAppService locationAppService
            )
        {
            _purchaseStorageAppService = purchaseStorageAppService;
            _warehouseLookupAppService = warehouseLookupAppService;
            _locationAppService = locationAppService;
            Model = new PurchaseStorageEditModel();
            Model.WarehouseSource = new ObservableCollection<WarehouseDto>();
        }
       

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouses = await _warehouseLookupAppService.GetAllAsync();
                foreach (var item in warehouses)
                {
                    this.Model.WarehouseSource.Add(item);
                }

                this.PageTitle = "采购入库-编辑";
                Guid id = (Guid)Model.Id;
                var result = await _purchaseStorageAppService.GetAsync(id);

                Model.Number = result.Number;
                Model.ArrivalNoticeNumber = result.ArrivalNoticeNumber;
                Model.Remark = result.Remark;
                Model.ProductId = result.ProductId;
                Model.ProductName = result.ProductName;
                Model.ProductUnitName = result.ProductUnitName;
                Model.ProductDefaultWarehouseId = result.ProductDefaultWarehouseId;
                Model.ProductDefaultLocationId = result.ProductDefaultLocationId;
                Model.ApplyQuantity = result.ApplyQuantity;
                Model.Details.Clear();

                foreach (var item in result.Details)
                {
                    PurchaseStorageDetailEditModel editModel = new PurchaseStorageDetailEditModel(Model);
                    editModel.Batch = item.Batch;
                    editModel.SelectedWarehouse = Model.WarehouseSource.Where(m=>m.Id == item.WarehouseId).FirstOrDefault();
                    if (editModel.SelectedWarehouse != null)
                    {
                        editModel.SelectedLocation = editModel.SelectedWarehouse.Locations.Where(m => m.Id == item.LocationId).FirstOrDefault();
                    }
                    editModel.Batch = item.Batch;
                    editModel.Quantity = item.Quantity;
                    Model.Details.Add(editModel);
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
     

        [AsyncCommand]
        public async Task SaveAsync()
        {
            await UpdateAsync();
            if (this.OnCloseWindowCallbackAsync != null)
            {
                await OnCloseWindowCallbackAsync();
            }
            this.Close();
        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            if (hasError == true) return !hasError;
            return true;
        }


        [Command]
        public void AddNewRow()
        {
            PurchaseStorageDetailEditModel detailEditModel = new PurchaseStorageDetailEditModel(this.Model);
            detailEditModel.Batch = this.Model.ArrivalNoticeNumber;
            detailEditModel.Quantity = 0;
            this.Model.Details.Add(detailEditModel);
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (this.Model.SelectedRow != null)
            {
                this.Model.Details.Remove(Model.SelectedRow);
                Model.NotifyTotalQuantityChanged();
            }
        }


        [Command]
        public void AutoStorageAll()
        {
            this.Model.Details.Clear();
            PurchaseStorageDetailEditModel detailEditModel = new PurchaseStorageDetailEditModel(this.Model);
            detailEditModel.Batch = this.Model.ArrivalNoticeNumber;
            detailEditModel.Quantity = Model.ApplyQuantity;
            detailEditModel.SelectedWarehouse = Model.WarehouseSource.Where(m=>m.Id == Model.ProductDefaultWarehouseId).First();
            detailEditModel.SelectedLocation = detailEditModel.SelectedWarehouse.Locations.Where(m => m.Id == Model.ProductDefaultLocationId).First();
            this.Model.Details.Add(detailEditModel);
        }

        public bool CanAutoStorageAll()
        {
            if (Model.ProductDefaultWarehouseId == null || Model.ProductDefaultLocationId == null)
            {
                return false;
            }
            return true;
        }

        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchaseStorageUpdateDto dto = new PurchaseStorageUpdateDto();
                dto.Remark = this.Model.Remark;
                for (int j = 0; j < Model.Details.Count; j++)
                {
                    var detail = Model.Details[j];
                    PurchaseStorageDetailUpdateDto detailDto = new PurchaseStorageDetailUpdateDto();
                    detailDto.Id = detail.Id;
                    if (detail.SelectedLocation != null)
                    {
                        detailDto.LocationId = detail.SelectedLocation.Id;
                    }
                    detailDto.Batch = detail.Batch;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _purchaseStorageAppService.UpdateAsync(Model.Id, dto);
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
    }
}
