using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;

[Serializable]
public class InventoryLogDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public DateTime LogTime { get; set; }

    public InventoryLogType LogType { get; set; }

    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; }

    public Guid LocationId { get; set; }
    public string LocationName { get; set; }


    public Guid ProductId { get; set; }
    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public string Batch { get; set; }


    public double InQuantity { get; set; }


    public double OutQuantity { get; set; }


    public double AfterQuantity { get; set; }
}