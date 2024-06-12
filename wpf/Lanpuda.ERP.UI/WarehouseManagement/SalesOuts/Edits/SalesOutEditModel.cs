using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Lanpuda.ERP.Permissions.ERPPermissions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Edits
{
    public class SalesOutEditModel : ModelBase
    {
        public Guid Id { get; set; }

        public string ShipmentApplyNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public Guid ProductId { get; set; }


        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }


        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

        public double TotalQuantity
        {
            get 
            {
                return this.Details.Sum(m => m.Quantity);
            }
        }

        public bool IsQuantityEqualsTotalQuantity
        {
            get
            {
                return  Quantity == TotalQuantity;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public SalesOutDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        /// <summary>
        /// 出库明细
        /// </summary>
        public ObservableCollection<SalesOutDetailEditModel> Details { get; set; }

        public SalesOutEditModel()
        {
            Details = new ObservableCollection<SalesOutDetailEditModel>();
        }

        public void NotifyTotalQuantityChanged()
        {
            RaisePropertyChanged(nameof(TotalQuantity));
            RaisePropertyChanged(nameof(IsQuantityEqualsTotalQuantity));
        }
    }

   

    public class SalesOutDetailEditModel : ModelBase
    {
        public SalesOutEditModel Model
        {
            get { return GetProperty(() => Model); }
            set { SetProperty(() => Model, value); }
        }
        public Guid? Id { get; set; }

        public Guid LocationId { get; set; }


        public string WarehouseName
        {
            get { return GetProperty(() => WarehouseName); }
            set { SetProperty(() => WarehouseName, value); }
        }

        public string LocationName
        {
            get { return GetProperty(() => LocationName); }
            set { SetProperty(() => LocationName, value); }
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


        public SalesOutDetailEditModel(SalesOutEditModel model)
        {
            this.Model = model;
        }
    }
}
