using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles
{
    public class SalesOrderProfileOutDetailDto
    {
        public Guid SalesOutDetailId { get; set; }

        public string SalesOutNumber { get; set; }

        public double Quantity { get; set; }

    }
}
