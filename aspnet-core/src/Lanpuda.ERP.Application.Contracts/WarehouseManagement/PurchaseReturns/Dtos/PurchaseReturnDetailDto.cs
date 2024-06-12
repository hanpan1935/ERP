using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;

[Serializable]
public class PurchaseReturnDetailDto : AuditedEntityDto<Guid>
{
    public Guid PurchaseReturnId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public Guid WarehouseId { get; set; }

    public string WarehouseName { get; set; }

    public Guid LocationId { get; set; }

    public string LocationName { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

}