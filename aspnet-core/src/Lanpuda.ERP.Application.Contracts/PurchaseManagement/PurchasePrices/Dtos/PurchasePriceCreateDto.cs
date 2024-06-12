using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;

[Serializable]
public class PurchasePriceCreateDto
{
    public Guid SupplierId { get; set; }

    public int AvgDeliveryDate { get; set; }

    public DateTime QuotationDate { get; set; }

    public string Remark { get; set; }

    public List<PurchasePriceDetailCreateDto> Details { get; set; }

    public PurchasePriceCreateDto()
    {
        Details = new List<PurchasePriceDetailCreateDto>();
    }
}