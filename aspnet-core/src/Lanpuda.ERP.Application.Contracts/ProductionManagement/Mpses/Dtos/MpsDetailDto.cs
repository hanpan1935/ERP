using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos;

[Serializable]
public class MpsDetailDto : AuditedEntityDto<Guid>
{
    public Guid MpsId { get; set; }

    public DateTime ProductionDate { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }
}