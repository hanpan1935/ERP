using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;

[Serializable]
public class InventoryCheckUpdateDto
{
    public DateTime CheckDate { get; set; }

    public string Discription { get; set; }

    public Guid WarehouseId { get; set; }

    public List<InventoryCheckDetailUpdateDto> Details { get; set; }

    public InventoryCheckUpdateDto()
    {
        Details = new List<InventoryCheckDetailUpdateDto>();
    }
}