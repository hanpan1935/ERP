using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies
{
    public enum PurchaseApplyType
    {
        [Display(Name = "手工创建", Description = "手工创建", Order = 1)]
        Manual = 0,


        [Display(Name = "生产计划", Description = "生产计划", Order = 2)]
        MPS = 1,


        [Display(Name = "安全库存", Description = "安全库存", Order = 3)]
        SafetyInventory = 2,
    }
}
