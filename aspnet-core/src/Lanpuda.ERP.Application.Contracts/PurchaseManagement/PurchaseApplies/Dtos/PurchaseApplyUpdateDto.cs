using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;

[Serializable]
public class PurchaseApplyUpdateDto
{
    public string Remark { get; set; }

    public List<PurchaseApplyDetailUpdateDto> Details { get; set; }

    public PurchaseApplyUpdateDto()
    {
        Details = new List<PurchaseApplyDetailUpdateDto>();
    }
}