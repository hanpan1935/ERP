using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles
{
    [Serializable]
    public class SalesOrderProfileShipmentApplyDetailDto
    {
        public Guid ShipmentApplyDetailId { get; set; }

        public string ShipmentApplyNumber { get; set; }

        public double Quantity { get; set; }

    }
}
