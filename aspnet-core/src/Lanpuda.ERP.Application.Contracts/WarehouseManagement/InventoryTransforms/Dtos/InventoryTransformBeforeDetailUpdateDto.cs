using System;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;

[Serializable]
public class InventoryTransformBeforeDetailUpdateDto
{
    public Guid LocationId { get; set; }

    public Guid ProductId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }
}