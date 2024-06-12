using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos
{
    public class PurchaseReturnApplyPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? SupplierName { get; set; }
    }
}
