using System;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos;

[Serializable]
public class SafetyInventoryUpdateDto
{
    public Guid ProductId { get; set; }

    public double? MinQuantity { get; set; }

    public double? MaxQuantity { get; set; }

}