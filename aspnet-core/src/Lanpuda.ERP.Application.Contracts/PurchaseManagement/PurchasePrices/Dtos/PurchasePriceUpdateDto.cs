using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;

[Serializable]
public class PurchasePriceUpdateDto
{
    public Guid SupplierId { get; set; }

    public int AvgDeliveryDate { get; set; }

    public DateTime QuotationDate { get; set; }

    public string Remark { get; set; }

    public List<PurchasePriceDetailUpdateDto> Details { get; set; }


    public PurchasePriceUpdateDto()
    {
        Details = new List<PurchasePriceDetailUpdateDto>();
    }
}