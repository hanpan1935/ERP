using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses.Edits
{
    public class WarehouseEditModel :ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid?>(nameof(Id)); }
            set { SetValue(value, nameof(Id)); }
        }

        [Required(ErrorMessage = "必填")]
        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        [Required(ErrorMessage = "必填")]
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

    }
}
