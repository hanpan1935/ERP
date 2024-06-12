using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.WarehouseManagement.Inventories
{
    public enum InventoryLogType
    {
        /// <summary>
        /// 采购入库(加库存)
        /// </summary>
        [Display(Name = "采购入库")]
        PurchaseStorage = 0,

        /// <summary>
        /// 采购退货(减库存)
        /// </summary>
        [Display(Name = "采购退货")]
        PurchaseReturn = 1,

        /// <summary>
        /// 销售出库(减库存)
        /// </summary>
        [Display(Name = "销售出库")]
        SalesOut = 2,
        /// <summary>
        /// 销售退货(加库存)
        /// </summary>
        [Display(Name = "销售退货")]
        SalesReturn = 3,
        /// <summary>
        /// 生产领料(减库存)
        /// </summary>
        [Display(Name = "生产领料")]
        WorkOrderOut = 4,

        /// <summary>
        /// 生产退料(加库存)
        /// </summary>
        [Display(Name = "生产退料")]
        WorkOrderReturn = 5,

        /// <summary>
        /// 生产入库(加库存)
        /// </summary>
        [Display(Name = "生产入库")]
        WorkOrderStorage = 6,

        /// <summary>
        /// 其他入库(加库存)
        /// </summary>
        [Display(Name = "其他入库")]
        OtherStorage = 7,

        /// <summary>
        /// 其他出库(减库存)
        /// </summary>
        /// 
        [Display(Name = "其他出库")]
        OtherOut = 8,

        /// <summary>
        /// 库存调拨-入库(加库存)
        /// </summary>
        /// 
        [Display(Name = "库存调拨-入库")]
        MoveIn = 9,

        /// <summary>
        /// 库存调拨-出库(减库存)
        /// </summary>
        [Display(Name = "库存调拨-出库")]
        MoveOut = 10,


        [Display(Name = "库盘点-盘盈")]
        InventoryCheckAdd = 11,


        [Display(Name = "库盘点-盘亏")]
        InventoryCheckReduce = 12,


        /// <summary>
        /// 形态转换-减少(减库存)
        /// </summary>
        [Display(Name = "形态转换-减少")]
        TransformAdd = 13,

        /// <summary>
        /// 库存盘点
        /// </summary>
        [Display(Name = "形态转换-增加")]
        TransformReduce = 14,

    }
}
