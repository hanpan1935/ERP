using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.WarehouseManagement.Inventories.Dtos
{
    public class InventoryListRequestDto
    {
        public Guid? ProductId { get; set; }
        public Guid? WarehourseId { get; set; }
        public Guid? LocationId { get; set; }
        public string Batch { get; set; }
    }
}
