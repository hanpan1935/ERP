using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos
{
    public class SalesReturnApplyPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public Guid? CustomerId { get; set; }

        public SalesReturnReason? Reason { get; set; }

        public bool? IsProductReturn { get; set; }

        public bool? IsConfirmed { get; set; }

    }
}
