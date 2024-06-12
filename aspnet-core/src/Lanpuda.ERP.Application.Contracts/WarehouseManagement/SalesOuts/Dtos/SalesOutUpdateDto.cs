using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;

[Serializable]
public class SalesOutUpdateDto
{
    public string Remark { get; set; }

    public List<SalesOutDetailUpdateDto> Details { get; set; }

    public SalesOutUpdateDto()
    {
        Details = new List<SalesOutDetailUpdateDto>();
    }
}