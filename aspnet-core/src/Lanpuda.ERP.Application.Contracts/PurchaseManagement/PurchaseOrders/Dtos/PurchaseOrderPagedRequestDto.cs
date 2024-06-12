using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos
{
    public class PurchaseOrderPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid? SupplierId { get; set; }

        public string? Number { get; set; }

        public DateTime? RequiredDateStart { get; set; }

        public DateTime? RequiredDateEnd { get; set; }

        public PurchaseOrderType? OrderType { get; set; }

        public PurchaseOrderCloseStatus? CloseStatus { get; set; }

        public bool? IsConfirmed { get; set; }
    }
}
