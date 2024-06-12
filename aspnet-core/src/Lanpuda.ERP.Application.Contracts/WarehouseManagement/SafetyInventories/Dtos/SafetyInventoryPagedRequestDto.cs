using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos
{
    public class SafetyInventoryPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid? ProductId { get; set; }

        public string? ProductName { get; set; } 
    }
}
