using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;

[Serializable]
public class InventoryCheckDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public DateTime CheckDate { get; set; }

    public string Discription { get; set; }

    public Guid WarehouseId { get; set; }

    public string WarehouseName { get; set; }


    public Guid? KeeperUserId { get; set; }
    public string KeeperUserSurname { get; set; }
    public string KeeperUserName { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime? SuccessfulTime { get; set; }

    public List<InventoryCheckDetailDto> Details { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}