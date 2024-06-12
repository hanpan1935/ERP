using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;

[Serializable]
public class OtherOutDetailDto : AuditedEntityDto<Guid>
{
    public Guid OtherOutId { get; set; }
   
    public Guid ProductId { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

    /////
    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductUnitName { get; set; }

    public string ProductSpec { get; set; }

    public Guid WarehouseId { get; set; }

    public string WarehouseName { get; set; }

    public string LocationName { get; set; }
}