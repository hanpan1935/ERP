using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders
{
    public enum WorkOrderCloseType
    {
        /// <summary>
        /// 待关闭
        /// </summary>
        ToBeClosed = 0,

        /// <summary>
        /// 系统关闭
        /// </summary>
        SystemClose = 1,

        /// <summary>
        /// 手动关闭
        /// </summary>
        ManualClose = 2,
    }
}
