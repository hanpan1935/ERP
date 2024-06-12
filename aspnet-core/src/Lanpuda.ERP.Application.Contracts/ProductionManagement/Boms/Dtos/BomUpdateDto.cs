using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos;

[Serializable]
public class BomUpdateDto
{
    public Guid ProductId { get; set; }


    public string Remark { set; get; }

    public List<BomDetailUpdateDto> Details { get; set; }


    public BomUpdateDto()
    {
        Details = new List<BomDetailUpdateDto>();
    }
}