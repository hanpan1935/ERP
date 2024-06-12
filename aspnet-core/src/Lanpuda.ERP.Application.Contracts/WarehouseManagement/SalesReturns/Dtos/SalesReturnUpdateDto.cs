using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos;

[Serializable]
public class SalesReturnUpdateDto
{
    public string Remark { get; set; }

    public List<SalesReturnDetailUpdateDto> Details { get; set; }

    public SalesReturnUpdateDto()
    {
        Details = new List<SalesReturnDetailUpdateDto>();
    }
}