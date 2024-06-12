using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks
{
    public enum InventoryCheckDetailType
    {
        /// <summary>
        /// 正常
        /// </summary>
        /// 
        [Display(Name = "正常")]
        None = 0,
        /// <summary>
        /// 盘盈
        /// </summary>
        /// 
        [Display(Name = "盘盈")]
        Add = 1,
        /// <summary>
        /// 盘亏
        /// </summary>
        /// 
        [Display(Name = "盘亏")]
        Reduce = 2,
    }
}
