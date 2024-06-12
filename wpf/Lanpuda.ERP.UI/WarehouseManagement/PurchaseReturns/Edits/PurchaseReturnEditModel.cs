using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Lanpuda.Client.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Edits
{
    public class PurchaseReturnEditModel : ModelBase
    {
        public Guid Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string PurchaseReturnApplyNumber
        {
            get { return GetProperty(() => PurchaseReturnApplyNumber); }
            set { SetProperty(() => PurchaseReturnApplyNumber, value); }
        }

     
       

        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }


        public Guid ProductId
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

        public string ProductUnitName
        {
            get { return GetProperty(() => ProductUnitName); }
            set { SetProperty(() => ProductUnitName, value); }
        }


        public double ApplyQuantity
        {
            get { return GetProperty(() => ApplyQuantity); }
            set { SetProperty(() => ApplyQuantity, value); }
        }

        public bool IsApplyQuantityEqualTotalQuantity
        {
            get
            {
                return ApplyQuantity == TotalQuantity;
            }
        }

        public double TotalQuantity
        {
            get
            {
                return this.Details.Sum(m => m.Quantity);
            }
        }

        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }


        public PurchaseReturnDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }


        public ObservableCollection<PurchaseReturnDetailEditModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }


        public PurchaseReturnEditModel()
        {
            Details = new ObservableCollection<PurchaseReturnDetailEditModel>();
        }

        public void NotifyTotalQuantityChanged()
        {
            RaisePropertyChanged(nameof(TotalQuantity));
            RaisePropertyChanged(nameof(IsApplyQuantityEqualTotalQuantity));
        }
    }




    public class PurchaseReturnDetailEditModel : ModelBase
    {

        public PurchaseReturnEditModel Model { get; set; }

        public Guid? Id { get; set; }


        [MaxLength(128)]
        public string? WarehouseName
        {
            get { return GetProperty(() => WarehouseName); }
            set { SetProperty(() => WarehouseName, value); }
        }


        [MaxLength(128)]
        public string? LocationName
        {
            get { return GetProperty(() => LocationName); }
            set { SetProperty(() => LocationName, value); }
        }


        public Guid LocationId { get; set; }


   


        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }


        /// <summary>
        /// 出库数量
        /// </summary>
        [Required]
        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value,Model.NotifyTotalQuantityChanged); }
        }


        public PurchaseReturnDetailEditModel(PurchaseReturnEditModel model)
        {
            Model = model;
        }
    }
}
