using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos
{
    public class PurchaseOrderDetailPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid? ProductId { get; set; }

        public bool? IsConfirmed { get; set; }

        public string? PurchaseOrderNumber { get; set; }

        public string? ProductName { get; set; }

        public Guid? SupplierId { get; set; }

        public string? SupplierName { get; set; }
    }
}
