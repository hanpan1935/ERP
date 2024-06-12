using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles
{
    public class SalesOrderProfileReturnDetailDto
    {
        public Guid SalesReturnDetailId { get; set; }

        public string SalesRetrunNumber { get; set; }

        public double Quantity { get; set; }

    }
}
