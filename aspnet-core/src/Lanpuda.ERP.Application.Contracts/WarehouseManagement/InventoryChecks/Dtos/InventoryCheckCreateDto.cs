using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;

[Serializable]
public class InventoryCheckCreateDto
{
    public DateTime CheckDate { get; set; }

    public string Discription { get; set; }

    public Guid WarehouseId { get; set; }

    public List<InventoryCheckDetailCreateDto> Details { get; set; }


    public InventoryCheckCreateDto()
    {
        Details = new List<InventoryCheckDetailCreateDto>();
    }
}