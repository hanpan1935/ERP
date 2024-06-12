using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.SalesManagement.SalesOrders
{
    public enum SalesOrderCloseStatus
    {

        /// <summary>
        /// 待关闭
        /// </summary>
        /// 
        [Display(Name = "待关闭")]
        ToBeClosed = 0,
        /// <summary>
        /// 系统关闭
        /// </summary>
        /// 
        [Display(Name = "系统关闭")]
        SystemClosed = 1,
        /// <summary>
        /// 手动关闭
        /// </summary>
        /// 
        [Display(Name = "手动关闭")]
        ManualClosed = 2
    }
}
