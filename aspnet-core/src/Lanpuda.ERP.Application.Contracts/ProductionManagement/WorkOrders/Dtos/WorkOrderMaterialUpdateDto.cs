using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos
{
    public class WorkOrderMaterialUpdateDto
    {
        public Guid? Id { get; set; }

        public Guid ProductId { get; set; }

        /// <summary>
        /// 计划用量
        /// </summary>
        public double Quantity { get; set; }
    }
}
