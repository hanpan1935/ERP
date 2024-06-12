using System;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;

[Serializable]
public class InventoryMoveDetailCreateDto
{
    public Guid ProductId { get; set; }

    public Guid OutLocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

    public Guid InLocationId { get; set; }

    public int Sort { get; set; }
}