using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos;

[Serializable]
public class BomDto : AuditedEntityDto<Guid>
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }


    public string Remark { set; get; }

    public List<BomDetailDto> Details { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
    public BomDto()
    {
        Details = new List<BomDetailDto>();
    }
}