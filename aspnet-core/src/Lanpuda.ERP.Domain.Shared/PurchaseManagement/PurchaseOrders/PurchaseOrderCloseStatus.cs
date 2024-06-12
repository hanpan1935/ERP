using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders
{
    public enum PurchaseOrderCloseStatus
    {
        /// <summary>
        /// 待关闭
        /// </summary>
        [Display(Name = "待关闭", Description = "进行中的采购订单", Order = 1)]
        Opened = 0,
        /// <summary>
        /// 系统关闭
        /// </summary>
        [Display(Name = "系统关闭", Description = "系统自动关闭的订单", Order = 2)]
        SystemClosed = 1,
        /// <summary>
        /// 手动关闭
        /// </summary>
        [Display(Name = "手动关闭", Description = "人为手动关闭的订单", Order = 3)]
        ManualClosed = 2
    }
}
