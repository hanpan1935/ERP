using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos
{
    public class WorkOrderAutoCreateDto
    {
        public Guid? BomDetailId { get; set; }

        public Guid ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string ProductSpec { get; set; }

        public string ProductUnitName { get; set; }

       

        public string MpsNumber { get; set; }


        public double Quantity { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime? CompletionDate { get; set; }
    }
}
