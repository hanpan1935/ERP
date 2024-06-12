using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;

[Serializable]
public class PurchaseReturnApplyCreateDto
{

    public Guid SupplierId { get; set; }

    public PurchaseReturnReason ReturnReason { get; set; }

    public string Description { get; set; }

    public string Remark { get; set; }

    public List<PurchaseReturnApplyDetailCreateDto> Details { get; set; }


    public PurchaseReturnApplyCreateDto()
    {
        this.Details = new List<PurchaseReturnApplyDetailCreateDto>();
    }
}