using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos
{
    public class WorkOrderMaterialDto
    {
        public Guid WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string ProductSpec { get; set; }
        public string ProductUnitName { get; set; }

        public ProductSourceType SourceType { get; set; }

        /// <summary>
        /// 计划用量
        /// </summary>
        public double Quantity { get; set; }
    }
}
