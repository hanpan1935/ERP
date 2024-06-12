using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;

[Serializable]
public class OtherStorageDetailDto : EntityDto<Guid>
{
    public Guid OtherStorageId { get; set; }

    public Guid ProductId { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

    public double Price { get; set; }


    /////////////////////////////////

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductUnitName { get; set; }

    public string ProductSpec { get; set; }

    public Guid WarehouseId { get; set; }

    public string WarehouseName { get; set; }

    public string LocationName { get; set; }
}