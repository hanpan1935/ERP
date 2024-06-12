using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders
{
    public enum WorkOrderType
    {
        //半成品工单

        [Display(Name = "半成品工单", Description = "半成品工单", Order = 1)]
        Semi = 0,

        //成品工单
        [Display(Name = "成品工单", Description = "成品工单", Order = 2)]
        Finished = 1,

        //委外工单
        [Display(Name = "委外工单", Description = "委外工单", Order = 3)]
        Outsourcing = 2,
    }
}
