using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;

[Serializable]
public class InventoryMoveCreateDto
{
    public string Reason { get; set; }

    public string Remark { get; set; }

    public List<InventoryMoveDetailCreateDto> Details { get; set; }

    public InventoryMoveCreateDto()
    {
        Details = new List<InventoryMoveDetailCreateDto>();
    }
}