using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos
{
    public class WorkOrderMultipleCreateDetailDto
    {
        public Guid? WorkshopId { get; set; }

        public Guid ProductId { get; set; }

        public double Quantity { get; set; }

        /// <summary>
        /// 开工日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }
    }
}
