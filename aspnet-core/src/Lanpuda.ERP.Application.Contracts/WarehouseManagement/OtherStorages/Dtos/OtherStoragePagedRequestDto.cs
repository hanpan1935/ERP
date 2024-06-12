using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;

[Serializable]
public class OtherStoragePagedRequestDto : PagedAndSortedResultRequestDto
{
    public string? Number { get; set; }

    public Guid? KeeperUserId { get; set; }

    public string? Description { get; set; }

    public bool? IsSuccessful { get; set; }

    public DateTime? SuccessfulTime { get; set; }

}