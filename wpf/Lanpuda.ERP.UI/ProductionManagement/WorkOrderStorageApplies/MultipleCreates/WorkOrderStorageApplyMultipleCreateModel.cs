using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Edits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.MultipleCreates
{
    partial class WorkOrderStorageApplyMultipleCreateModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

     
        public WorkOrderStorageApplyMultipleCreateModel()
        {
        }
    }


    public class WorkOrderStorageApplyMultipleCreateDetailModel : ModelBase 
    {
        public Guid? Id { get; set; }

        public Guid WorkOrderId
        {
            get { return GetProperty(() => WorkOrderId); }
            set { SetProperty(() => WorkOrderId, value); }
        }

        [Required]
        public string? WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
        }


        public double WorkOrderQuantity
        {
            get { return GetProperty(() => WorkOrderQuantity); }
            set { SetProperty(() => WorkOrderQuantity, value); }
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

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }
    }
}
