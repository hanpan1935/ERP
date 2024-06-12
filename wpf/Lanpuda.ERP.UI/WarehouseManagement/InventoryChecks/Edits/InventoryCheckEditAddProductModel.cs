using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.UI.WarehouseManagement.InventoryChecks.Edits
{
    public class InventoryCheckEditAddProductModel : ModelBase
    {
        public Guid ProductId { get; set; }

        public string ProductNumber
        {
            get { return GetProperty(() => ProductNumber); }
            set { SetProperty(() => ProductNumber, value); }
        }

        public string ProductSpec
        {
            get { return GetProperty(() => ProductSpec); }
            set { SetProperty(() => ProductSpec, value); }
        }

        public string ProductUnitName
        {
            get { return GetProperty(() => ProductUnitName); }
            set { SetProperty(() => ProductUnitName, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        [GuidNotEmpty(ErrorMessage = "必填")]
        public Guid LocationId
        {
            get { return GetProperty(() => LocationId); }
            set { SetProperty(() => LocationId, value, OnLocationIdChanged); }
        }


        public string LocationName
        {
            get { return GetProperty(() => LocationName); }
            set { SetProperty(() => LocationName, value); }
        }


        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        public ObservableCollection<LocationDto> Locations { get; set; }

        public InventoryCheckEditAddProductModel()
        {
            Locations = new ObservableCollection<LocationDto>();
        }

        private void OnLocationIdChanged()
        {
            var location = Locations.Where(m=>m.Id ==  LocationId).FirstOrDefault();
            if (location != null)
            {
                this.LocationName = location.Name;
            }
        }
    }
}
