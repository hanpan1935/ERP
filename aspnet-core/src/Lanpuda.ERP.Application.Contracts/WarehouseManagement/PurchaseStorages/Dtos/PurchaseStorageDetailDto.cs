using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;

[Serializable]
public class PurchaseStorageDetailDto : EntityDto<Guid>
{
    public Guid PurchaseStorageId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public Guid WarehouseId { get; set; }

    public string WarhouseName { get; set; }


    public Guid LocationId { get; set; }

    public string LocationName { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

    public int Sort { get; set; }
}