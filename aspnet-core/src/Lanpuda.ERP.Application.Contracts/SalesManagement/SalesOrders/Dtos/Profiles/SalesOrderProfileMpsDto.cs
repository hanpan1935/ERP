using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles
{
    public class SalesOrderProfileMpsDto
    {
        public Guid MpsId { get; set; }

        public string MpsNumber { get; set; }

        public double Quanity { get; set; }
    }
}
