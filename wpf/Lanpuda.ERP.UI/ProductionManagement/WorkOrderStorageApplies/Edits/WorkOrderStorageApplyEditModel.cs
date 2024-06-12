using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Edits
{
    public class WorkOrderStorageApplyEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid WorkOrderId { get; set; }

        public string WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
        }

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

       
        public WorkOrderStorageApplyEditModel()
        {
           
        }
    }

   
}
