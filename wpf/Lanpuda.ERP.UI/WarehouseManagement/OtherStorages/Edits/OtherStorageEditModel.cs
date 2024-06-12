using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages.Edits
{
    public class OtherStorageEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        [Required(ErrorMessage = "必填")]
        public string Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }


        public OtherStorageDetailEditModel? SelectedDetailRow
        {
            get { return GetProperty(() => SelectedDetailRow); }
            set { SetProperty(() => SelectedDetailRow, value); }
        }

        public ObservableCollection<OtherStorageDetailEditModel> Details { get; set; }

        public OtherStorageEditModel()
        {
            Details = new ObservableCollection<OtherStorageDetailEditModel>();
        }

    }


    public class OtherStorageDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }
        public Guid ProductId { get; set; }

        public Guid LocationId
        {
            get { return GetProperty(() => LocationId); }
            set { SetProperty(() => LocationId, value); }
        }

        [MaxLength(128)]
        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }


        public double Price
        {
            get { return GetProperty(() => Price); }
            set { SetProperty(() => Price, value); }
        }


        /// <summary>
        /// /////////////////////////
        /// </summary>
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


        public WarehouseDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse ,value, OnSelectedWarehouseChanged); }
        }

        public ObservableCollection<WarehouseDto> WarehouseSource { get; set; }


        public OtherStorageDetailEditModel(List<WarehouseDto> warehouseList)
        {
            WarehouseSource = new ObservableCollection<WarehouseDto>(warehouseList);
            SelectedWarehouse = WarehouseSource.FirstOrDefault();
        }


        private void OnSelectedWarehouseChanged()
        {
            if (SelectedWarehouse != null)
            {
                var location = SelectedWarehouse.Locations.FirstOrDefault();
                if (location != null)
                {
                    this.LocationId = location.Id;
                }
            }
           
        }
    }
}
