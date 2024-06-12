using HandyControl.Collections;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Edits
{
    public class WorkOrderStorageEditModel : ModelBase
    {
        public Guid Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string? ApplyNumber
        {
            get { return GetProperty(() => ApplyNumber); }
            set { SetProperty(() => ApplyNumber, value); }
        }

        public string WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
        }

        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public Guid ProductId { get; set; }
        public Guid? ProductDefaultLocationId { get; set; }
        public Guid? ProductDefaultWarehouseId { get; set; }

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public WorkOrderStorageDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<WorkOrderStorageDetailEditModel> Details { get; set; }

        public WorkOrderStorageEditModel()
        {
            Details = new ObservableCollection<WorkOrderStorageDetailEditModel>();
        }

        public ManualObservableCollection<WarehouseDto> WarehouseSource
        {
            get { return GetProperty(() => WarehouseSource); }
            set { SetProperty(() => WarehouseSource, value); }
        }
    }


    public class WorkOrderStorageDetailEditModel : ModelBase
    {

        public WorkOrderStorageEditModel Model { get; set; }

        public Guid? Id { get; set; }


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


        [MaxLength(128)]
        public string? Batch
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

        public WorkOrderStorageDetailEditModel(WorkOrderStorageEditModel model)
        {
            Model = model;
        }
    }
}
