using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;

[Serializable]
public class InventoryDto : AuditedEntityDto<Guid>
{
    public string WarehouseName { get; set; }

    public Guid LocationId { get; set; }

    public string LocationName { get; set; }

    public Guid ProductId { get; set; }
    public string ProductNumber { get; set; }

    public string ProductName { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public double Quantity { get; set; }

    public string Batch { get; set; }
 

    //public decimal Price { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}