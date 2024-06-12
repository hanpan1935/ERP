using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.BasicData.Products
{
    public enum ProductSourceType
    {
        /// <summary>
        /// 自制 - 半成品
        /// </summary>
        [Display(Name = "自制", Description = "自制", Order = 1)]
        Self = 0,

        /// <summary>
        /// 采购 -  原材料
        /// </summary>
        [Display(Name = "采购", Description = "采购", Order = 2)]
        Purchase = 1,


        /// <summary>
        /// 委外 - 原材料
        /// </summary>
        [Display(Name = "委外", Description = "委外", Order = 3)]
        Outsourcing = 2,

        /// <summary>
        /// 客供 - 原材料
        /// </summary>
        [Display(Name = "客供", Description = "客供", Order = 4)]
        Customer = 3,


        /// <summary>
        /// 虚拟 - 原材料
        /// </summary>
        [Display(Name = "虚拟", Description = "虚拟", Order = 5)]
        Fictitious = 4,
    }
}
