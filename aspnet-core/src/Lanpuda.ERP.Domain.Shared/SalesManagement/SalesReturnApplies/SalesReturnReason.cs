using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies
{
    public enum SalesReturnReason
    {
        /// <summary>
        /// 质量问题
        /// </summary>
        /// 
        [Display(Name = "质量问题")]
        Quality = 0,

        /// <summary>
        /// 协商退货
        /// </summary>
        /// 
        [Display(Name = "协商退货")]
        Negotiate = 1,


        /// <summary>
        /// 其他
        /// </summary>
        /// 
        [Display(Name = "其他")]
        Other = 2,
    }
}
