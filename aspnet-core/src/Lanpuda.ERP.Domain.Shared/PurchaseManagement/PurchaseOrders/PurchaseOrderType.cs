using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders
{
    public enum PurchaseOrderType
    {
        /// <summary>
        /// 手工订单
        /// </summary>
        [Display(Name = "手工订单", Description = "手工创建的订单", Order = 1)]
        ManualOrder = 0,
        /// <summary>
        /// 采购申请
        /// </summary>
        [Display(Name = "采购申请", Description = "申购申请创建的订单", Order = 2)]
        PurchaseRequest = 1,
        /// <summary>
        /// 生产计划
        /// </summary>
        [Display(Name = "生产计划", Description = "生产计划创建的订单", Order = 3)]
        ProductionPlan = 2,
        /// <summary>
        /// 销售订单
        /// </summary>
        [Display(Name = "销售订单", Description = "销售订单创建的订单", Order = 4)]
        SalesOrder = 3,
        /// <summary>
        /// 安全库存
        /// </summary>
        [Display(Name = "安全库存", Description = "安全库存创建的订单", Order = 5)]
        SafetyStock = 4,
        /// <summary>
        /// 委外加工
        /// </summary>
        [Display(Name = "委外加工", Description = "委外加工创建的订单", Order = 6)]
        Outsourcing = 5
    }
}
