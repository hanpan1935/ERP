using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Inventories.Dtos
{
    public class InventoryLogPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public Guid? ProductId { get; set; }

        public string? ProductName { get; set; } 

        public DateTime? FlowTimeStart { get; set; }

        public DateTime? FlowTimeEnd { get; set; }

        public InventoryLogType? LogType { get; set; }

        public string? Batch { get; set; }

    }
}
