using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;

[Serializable]
public class InventoryTransformDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public string Reason { get; set; }

    public Guid KeeperUserId { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime? SuccessfulTime { get; set; }

    public List<InventoryTransformBeforeDetailDto> BeforeDetails { get; set; }

    public List<InventoryTransformAfterDetailDto> AfterDetails { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}