using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;

[Serializable]
public class MaterialReturnApplyUpdateDto
{
    public string Remark { get; set; }

    public List<MaterialReturnApplyDetailUpdateDto> Details { get; set; }

    public MaterialReturnApplyUpdateDto()
    {
        Details = new List<MaterialReturnApplyDetailUpdateDto>();
    }
}