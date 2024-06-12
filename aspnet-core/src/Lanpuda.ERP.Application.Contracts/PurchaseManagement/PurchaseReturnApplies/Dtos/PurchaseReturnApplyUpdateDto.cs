using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;

[Serializable]
public class PurchaseReturnApplyUpdateDto
{
    public Guid SupplierId { get; set; }

    public PurchaseReturnReason ReturnReason { get; set; }

    public string Description { get; set; }

    public string Remark { get; set; }

 

    public List<PurchaseReturnApplyDetailUpdateDto> Details { get; set; }

    public PurchaseReturnApplyUpdateDto()
    {
        this.Details = new List<PurchaseReturnApplyDetailUpdateDto>();
    }
}