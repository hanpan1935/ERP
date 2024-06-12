using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves.Edits
{
    public class InventoryMoveEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public string Reason
        {
            get { return GetProperty(() => Reason); }
            set { SetProperty(() => Reason, value); }
        }

        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }


        public InventoryMoveDetailEditModel? SelectedDetailRow
        {
            get { return GetProperty(() => SelectedDetailRow); }
            set { SetProperty(() => SelectedDetailRow, value); }
        }

        public ObservableCollection<InventoryMoveDetailEditModel> Details { get; set; }

        public InventoryMoveEditModel()
        {
            Details = new ObservableCollection<InventoryMoveDetailEditModel>();
        }
    }


    public class InventoryMoveDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public Guid ProductId { get; set; }


        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductNumber
        {
            get { return GetProperty(() => ProductNumber); }
            set { SetProperty(() => ProductNumber, value); }
        }

        public string ProductSpec
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string ProductUnitName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string OutWarehouseName
        {
            get { return GetProperty(() => OutWarehouseName); }
            set { SetProperty(() => OutWarehouseName, value); }
        }


        public string OutLocationName
        {
            get { return GetProperty(() => OutLocationName); }
            set { SetProperty(() => OutLocationName, value); }
        }

        public Guid OutLocationId
        {
            get { return GetProperty(() => OutLocationId); }
            set { SetProperty(() => OutLocationId, value); }
        }

        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

       


        public WarehouseDto? SelectedInWarehouse
        {
            get { return GetProperty(() => SelectedInWarehouse); }
            set { SetProperty(() => SelectedInWarehouse, value); }
        }

        public Guid InLocationId
        {
            get { return GetProperty(() => InLocationId); }
            set { SetProperty(() => InLocationId, value); }
        }


        public ObservableCollection<WarehouseDto> WarehouseSource { get; set; }

        public InventoryMoveDetailEditModel(List<WarehouseDto> warehouseList)
        {
            WarehouseSource = new ObservableCollection<WarehouseDto>(warehouseList);
        }
       
    }
}
