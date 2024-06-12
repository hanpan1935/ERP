using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;

[Serializable]
public class PurchaseReturnUpdateDto
{
    public string Remark { get; set; }

    public List<PurchaseReturnDetailUpdateDto> Details { get; set; }

    public PurchaseReturnUpdateDto()
    {
        Details = new List<PurchaseReturnDetailUpdateDto>();
    }
}