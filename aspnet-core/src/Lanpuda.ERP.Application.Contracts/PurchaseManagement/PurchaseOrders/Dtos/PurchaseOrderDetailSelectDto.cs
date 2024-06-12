using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos
{
    public class PurchaseOrderDetailSelectDto : EntityDto<Guid>
    {
        public Guid PurchaseOrderId { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string SupplierFullName { get; set; }

        public string SupplierShortName { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductNumber { get; set; }

        public string ProductSpec { get; set; }

        public string ProductUnitName { get; set; }

        public DateTime PromiseDate { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }

        public double TaxRate { get; set; }

        public string Remark { get; set; }

    }
}
