using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.ProductionManagement.Mpses
{
    public enum MpsType
    {
        //客户订单

        [Display(Name = "客户订单", Description = "客户订单", Order = 1)]
        Customer = 0,
        //内部订单
        [Display(Name = "内部订单", Description = "内部订单", Order = 2)]
        Internal = 1,
        //安全库存
        [Display(Name = "安全库存", Description = "安全库存", Order = 3)]
        SafetyInventory = 2,
    }
}
