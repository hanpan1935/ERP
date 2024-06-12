using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos;

[Serializable]
public class SafetyInventoryDto : AuditedEntityDto<Guid>
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductUnitName { get; set; }

    public string ProductSpec { get; set; }

    public double? MinQuantity { get; set; }

    public double? MaxQuantity { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}