using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;

[Serializable]
public class SalesReturnApplyUpdateDto
{
    public Guid CustomerId { get; set; }

    public SalesReturnReason Reason { get; set; }

    public bool IsProductReturn { get; set; }

    public string Description { get; set; }

    public List<SalesReturnApplyDetailUpdateDto> Details { get; set; }

    public SalesReturnApplyUpdateDto()
    {
        Details = new List<SalesReturnApplyDetailUpdateDto>();
    }
}