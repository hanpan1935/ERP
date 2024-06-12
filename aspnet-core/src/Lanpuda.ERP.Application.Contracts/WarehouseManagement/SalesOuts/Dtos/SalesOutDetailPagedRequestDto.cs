using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos
{
    public class SalesOutDetailPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string SalesOutNumber { get; set; }

        public string ProductName { get; set; }

        public string Batch { get; set; }

        public Guid? CustomerId { get; set; }
    }
}
