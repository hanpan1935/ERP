using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;
using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using HandyControl.Collections;
using System.Drawing.Printing;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using DevExpress.Mvvm.DataAnnotations;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Edits
{
    public class WorkOrderReturnEditModel : ModelBase
    {
        public Guid Id { get; set; }

        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }



        public Guid ProductId { get; set; }
        public Guid? ProductDefaultLocationId { get; set; }
        public Guid? ProductDefaultWarehouseId { get; set; }
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

        public string ProductUnitName
        {
            get { return GetProperty(() => ProductUnitName); }
            set { SetProperty(() => ProductUnitName, value); }
        }

        /// <summary>
        /// 退料数量
        /// </summary>
        public double ApplyQuantity
        {
            get { return GetProperty(() => ApplyQuantity); }
            set { SetProperty(() => ApplyQuantity, value); }
        }

        public double TotalQuantity
        {
            get
            {
                var total = this.Details.Sum(m => m.Quantity);
                return total;
            }
        }


        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        public WorkOrderReturnDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<WorkOrderReturnDetailEditModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }


        public WorkOrderReturnEditModel()
        {
            Details = new ObservableCollection<WorkOrderReturnDetailEditModel>();
        }

        public ManualObservableCollection<WarehouseDto> WarehouseSource
        {
            get { return GetProperty(() => WarehouseSource); }
            set { SetProperty(() => WarehouseSource, value); }
        }


        public bool IsTotalQuantityEqualApplyQuantity
        {
            get
            {
                if (TotalQuantity == ApplyQuantity)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void NotifyTotalQuantityChanged()
        {
            RaisePropertyChanged(nameof(TotalQuantity));
            RaisePropertyChanged(nameof(IsTotalQuantityEqualApplyQuantity));
        }

    }





    public class WorkOrderReturnDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }

      
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
            set { SetProperty(() => Quantity, value, Model.NotifyTotalQuantityChanged); }
        }

        [Required(ErrorMessage ="必填")]
        public WarehouseDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value); }
        }


        [Required(ErrorMessage = "必填")]
        public LocationDto? SelectedLocation
        {
            get { return GetProperty(() => SelectedLocation); }
            set { SetProperty(() => SelectedLocation, value); }
        }


        public WorkOrderReturnEditModel Model { get; set; }

        public WorkOrderReturnDetailEditModel(WorkOrderReturnEditModel model)
        {
            Model = model;
        }

    }
}
