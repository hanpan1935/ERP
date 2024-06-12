using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;

[Serializable]
public class MaterialApplyCreateDto
{
    public string Remark { get; set; }

    public Guid WorkOrderId { get; set; }

    public List<MaterialApplyDetailCreateDto> Details { get; set; }

    public MaterialApplyCreateDto()
    {
        Details = new List<MaterialApplyDetailCreateDto>();
    }
}