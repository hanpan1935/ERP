using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Inventories.Dtos
{
    public class InventoryPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid? WarehouseId { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? ProductSpec { get; set; }

        public string? Batch { get; set; }

    }
}
