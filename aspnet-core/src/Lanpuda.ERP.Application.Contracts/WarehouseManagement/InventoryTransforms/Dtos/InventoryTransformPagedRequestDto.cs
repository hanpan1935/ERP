using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;

[Serializable]
public class InventoryTransformPagedRequestDto : PagedAndSortedResultRequestDto
{
    public string? Number { get; set; }

    public string? Reason { get; set; }

    public Guid? KeeperUserId { get; set; }

    public bool? IsSuccessful { get; set; }

    public DateTime? SuccessfulTime { get; set; }

}