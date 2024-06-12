using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;

[Serializable]
public class PurchaseApplyCreateDto
{

    public string Remark { get; set; }
   

    public List<PurchaseApplyDetailCreateDto> Details { get; set; }

    public PurchaseApplyCreateDto()
    {
        Details = new List<PurchaseApplyDetailCreateDto>();
    }
}