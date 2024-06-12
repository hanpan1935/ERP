using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;

[Serializable]
public class InventoryTransformCreateDto
{
    public string Reason { get; set; }

    public List<InventoryTransformBeforeDetailCreateDto> BeforeDetails { get; set; }

    public List<InventoryTransformAfterDetailCreateDto> AfterDetails { get; set; }
}