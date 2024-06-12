using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Inventories.Dtos
{
    public class InventoryPagedResultDto : PagedResultDto<InventoryDto>
    {
        public double TotalQuantity { get; set; }

        public InventoryPagedResultDto(long totalCount, IReadOnlyList<InventoryDto> items)
        {
            TotalCount = totalCount;
            base.Items = items;
        }
    }
}
