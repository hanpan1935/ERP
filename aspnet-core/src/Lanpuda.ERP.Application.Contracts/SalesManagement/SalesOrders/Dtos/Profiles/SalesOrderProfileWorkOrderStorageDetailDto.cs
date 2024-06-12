using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles
{
    public class SalesOrderProfileWorkOrderStorageDetailDto
    {
        public Guid WorkOrderStorageDetailId { get; set; }
        public string WorkOrderStorageNumber { get; set; }
        public double Quantity { get; set; }
    }
}
