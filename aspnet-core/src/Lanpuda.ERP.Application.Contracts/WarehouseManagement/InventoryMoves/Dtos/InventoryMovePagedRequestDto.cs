using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;

[Serializable]
public class InventoryMovePagedRequestDto : PagedAndSortedResultRequestDto
{
    public string? Number { get; set; }

    public Guid? KeeperUserId { get; set; }

    public string? Reason { get; set; }

    public string? Remark { get; set; }

    public bool? IsSuccessful { get; set; }

    public DateTime? SuccessfulTime { get; set; }

}