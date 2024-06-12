using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Workshops.Dtos;

[Serializable]
public class WorkshopDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public string Name { get; set; }

    public string Remark { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}