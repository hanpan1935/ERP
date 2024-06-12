using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;

[Serializable]
public class InventoryMoveDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid? KeeperUserId { get; set; }

    public string KeeperUserSurname { get; set; }

    public string KeeperUserName { get; set; }

    public string Reason { get; set; }

    public string Remark { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime? SuccessfulTime { get; set; }

    public List<InventoryMoveDetailDto> Details { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public InventoryMoveDto()
    {
        Details = new List<InventoryMoveDetailDto>();
    }
}