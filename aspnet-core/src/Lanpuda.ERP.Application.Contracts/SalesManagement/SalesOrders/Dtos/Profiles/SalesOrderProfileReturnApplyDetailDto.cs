using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles
{
    [Serializable]
    public class SalesOrderProfileReturnApplyDetailDto
    {
        public Guid SalesReturnApplyDetailId { get; set; }

        public string SalesReturnApplyNumber { get; set; }

        public double Quantity { get; set; }
    }
}
