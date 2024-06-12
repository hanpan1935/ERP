using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Timing;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Dtos
{
    public class SalesPricePagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? CustomerName { get; set; }
        
        public DateTime? ValidDate { get; set; }

    }
}
