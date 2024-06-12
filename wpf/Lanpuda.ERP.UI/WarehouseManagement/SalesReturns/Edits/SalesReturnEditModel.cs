using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Collections;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Edits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns.Edits
{
    public class SalesReturnEditModel : ModelBase
    {
        public Guid Id { get; set; }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public Guid ProductId { get; set; }
        public Guid? ProductDefaultLocationId { get; set; }
        public Guid? ProductDefaultWarehouseId { get; set; }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }


       


        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }


        public double ApplyQuantity
        {
            get { return GetProperty(() => ApplyQuantity); }
            set { SetProperty(() => ApplyQuantity, value); }
        }

        public SalesReturnDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }


        /// <summary>
        /// 申请明细
        /// </summary>
        public ObservableCollection<SalesReturnDetailEditModel> Details { get; set; }

        public SalesReturnEditModel()
        {
            Details = new ObservableCollection<SalesReturnDetailEditModel>();
        }

        public ManualObservableCollection<WarehouseDto> WarehouseSource
        {
            get { return GetProperty(() => WarehouseSource); }
            set { SetProperty(() => WarehouseSource, value); }
        }

        public double TotalQuantity
        {
            get
            {
                var total = this.Details.Sum(m => m.Quantity);
                return total;
            }
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

    

    public class SalesReturnDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }
        public SalesReturnEditModel Model { get; set; }

        public WarehouseDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value); }
        }

        [Required(ErrorMessage ="必填")]
        public LocationDto? SelectedLocation
        {
            get { return GetProperty(() => SelectedLocation); }
            set { SetProperty(() => SelectedLocation, value); }
        }


        /// <summary>
        /// 发货数量
        /// </summary>
        public double Quantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value, Model.NotifyTotalQuantityChanged); }
        }


        /// <summary>
        /// 批次
        /// </summary>
        public string Batch
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public SalesReturnDetailEditModel(SalesReturnEditModel model)
        {
            Model = model;
        }
    }
}
