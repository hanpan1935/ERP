using System;
using System.Collections.Generic;
using System.Diagnostics;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;

[Serializable]
public class OtherStorageDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid? KeeperUserId { get; set; }

    public string KeeperUserSurname { get; set; }

    public string KeeperUserName { get; set; }

    public string Description { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime? SuccessfulTime { get; set; }

    public List<OtherStorageDetailDto> Details { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}