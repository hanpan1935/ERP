using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos
{
    public class SalesOrderDetailSelectDto : EntityDto<Guid>
    {
        public Guid SalesOrderId { get; set; }

        public string SalesOrderNumber { get; set; }

        public DateTime DeliveryDate { get; set; }

        public double Quantity { get; set; }

        public Guid ProductId { get; set; }

        public double Price { get; set; }

        public double TaxRate { get; set; }

        public string Requirement { get; set; }

        public string ProductName { get; set; }

        public string ProductNumber { get; set; }

        public string ProductUnitName { get; set; }

        public string ProductSpec { get; set; }

    }
}
