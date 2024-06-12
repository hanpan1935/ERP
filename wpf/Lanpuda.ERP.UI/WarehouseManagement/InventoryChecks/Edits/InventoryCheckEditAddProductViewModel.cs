using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks.Edits;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Locations;

namespace Lanpuda.ERP.UI.WarehouseManagement.InventoryChecks.Edits
{
    public class InventoryCheckEditAddProductViewModel : EditViewModelBase<InventoryCheckEditAddProductModel>
    {
        public Action<InventoryCheckEditAddProductModel>? SaveCallback;
        private readonly ILocationAppService _locationAppService;

        public Guid WarehouseId { get; set; }
        
        public InventoryCheckEditAddProductViewModel(ILocationAppService locationAppService)
        {
            _locationAppService = locationAppService;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouseList = await _locationAppService.GetByWarehouseIdAsync(WarehouseId);
                foreach (var item in warehouseList)
                {
                    Model.Locations.Add(item);
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


        public void ShowSelectProductWindow()
        {

        }




        [Command]
        public void Save()
        {
            if (SaveCallback != null)
            {
                SaveCallback(this.Model);
                this.CurrentWindowService.Close();
            }
        }


        public bool CanSave()
        {
            if (this.Model.ProductId == Guid.Empty)
            {
                return false;
            }
            if (this.Model.LocationId == Guid.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
