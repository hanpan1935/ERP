using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.BasicData.ProductUnits.Dtos;

[Serializable]
public class ProductUnitDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public string Number { get; set; }

    public string Remark { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}