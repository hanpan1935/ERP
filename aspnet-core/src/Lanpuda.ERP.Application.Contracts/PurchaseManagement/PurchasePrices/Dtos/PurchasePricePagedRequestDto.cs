using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos
{
    public class PurchasePricePagedRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid? SupplierId { get; set; }

        public string? Number { get; set; }

        public DateTime? QuotationDateStart { get; set; }

        public DateTime? QuotationDateEnd { get; set; }
    }
}
