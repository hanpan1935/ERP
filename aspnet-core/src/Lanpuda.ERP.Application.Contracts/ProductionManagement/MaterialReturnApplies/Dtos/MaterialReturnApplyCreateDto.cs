using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;

[Serializable]
public class MaterialReturnApplyCreateDto
{
    public string Remark { get; set; }

    public List<MaterialReturnApplyDetailCreateDto> Details { get; set; }

    public MaterialReturnApplyCreateDto()
    {
        Details = new List<MaterialReturnApplyDetailCreateDto>();
    }
}