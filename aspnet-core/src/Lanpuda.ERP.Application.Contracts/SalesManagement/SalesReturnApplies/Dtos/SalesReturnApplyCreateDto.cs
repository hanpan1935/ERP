using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;

[Serializable]
public class SalesReturnApplyCreateDto
{
    public Guid CustomerId { get; set; }

    public SalesReturnReason Reason { get; set; }

    public bool IsProductReturn { get; set; }

    public string Description { get; set; }

    public List<SalesReturnApplyDetailCreateDto> Details { get; set; }

    public SalesReturnApplyCreateDto()
    {
        Details = new List<SalesReturnApplyDetailCreateDto>();
    }
}