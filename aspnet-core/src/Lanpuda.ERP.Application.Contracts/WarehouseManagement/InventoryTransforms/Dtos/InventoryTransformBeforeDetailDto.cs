using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;

[Serializable]
public class InventoryTransformBeforeDetailDto : AuditedEntityDto<Guid>
{
    public Guid InventoryTransformId { get; set; }

    public Guid LocationId { get; set; }

    public Guid ProductId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }
}