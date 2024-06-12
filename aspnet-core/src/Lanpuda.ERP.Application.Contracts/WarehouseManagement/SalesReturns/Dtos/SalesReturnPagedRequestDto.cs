using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos
{
    public class SalesReturnPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? ApplyNumber { get; set; }

        public string? CustomerName { get; set; }

        public string? ProductName { get; set; }


        public SalesReturnReason? Reason { get; set; }

        public bool? IsSuccessful { get; set; }
    }
}
