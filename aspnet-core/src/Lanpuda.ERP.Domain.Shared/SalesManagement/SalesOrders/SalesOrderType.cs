using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.SalesManagement.SalesOrders
{
    public enum SalesOrderType
    {
        /// <summary>
        /// 正式订单
        /// </summary>
        [Display(Name = "正式订单")]
        Formal = 0,
        /// <summary>
        /// 样品订单
        /// </summary>
        /// 
        [Display(Name = "样品订单")]
        Sample = 1,
        /// <summary>
        /// 返点订单
        /// </summary>
        [Display(Name = "返点订单")]
        Rebate = 2
    }
}
