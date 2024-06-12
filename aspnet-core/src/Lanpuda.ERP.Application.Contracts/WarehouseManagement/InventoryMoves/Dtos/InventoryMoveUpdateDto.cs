using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;

[Serializable]
public class InventoryMoveUpdateDto
{
    public string Reason { get; set; }

    public string Remark { get; set; }

    public List<InventoryMoveDetailUpdateDto> Details { get; set; }

    public InventoryMoveUpdateDto()
    {
        Details= new List<InventoryMoveDetailUpdateDto>();
    }
}