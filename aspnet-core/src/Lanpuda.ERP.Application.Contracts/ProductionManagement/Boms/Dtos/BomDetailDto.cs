using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos;

[Serializable]
public class BomDetailDto : AuditedEntityDto<Guid>
{
    public Guid BomId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }

}