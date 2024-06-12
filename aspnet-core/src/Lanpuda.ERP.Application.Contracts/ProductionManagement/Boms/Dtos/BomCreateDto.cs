using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos;

[Serializable]
public class BomCreateDto
{
    public Guid ProductId { get; set; }


    public string Remark { set; get; }

    public List<BomDetailCreateDto> Details { get; set; }

    public BomCreateDto()
    {
        Details = new List<BomDetailCreateDto>();
    }
}