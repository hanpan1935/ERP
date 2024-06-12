using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;

[Serializable]
public class InventoryTransformUpdateDto
{
    public string Reason { get; set; }

    public DateTime? SuccessfulTime { get; set; }

    public List<InventoryTransformBeforeDetailUpdateDto> BeforeDetails { get; set; }

    public List<InventoryTransformAfterDetailUpdateDto> AfterDetails { get; set; }
}