using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;

[Serializable]
public class InventoryCheckPagedRequestDto : PagedAndSortedResultRequestDto
{
    public string? Number { get; set; }

    public DateTime? CheckDate { get; set; }

    public Guid? KeeperUserId { get; set; }

    public bool? IsSuccessful { get; set; }

    public DateTime? SuccessfulTime { get; set; }

    public Guid? WarehouseId { get; set; }

}