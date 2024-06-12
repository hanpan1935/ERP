using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;

[Serializable]
public class MaterialApplyUpdateDto
{
    public Guid WorkOrderId { get; set; }

    public string Remark { get; set; }

    public List<MaterialApplyDetailUpdateDto> Details { get; set; }

    public MaterialApplyUpdateDto()
    {
        Details = new List<MaterialApplyDetailUpdateDto>();
    }
}