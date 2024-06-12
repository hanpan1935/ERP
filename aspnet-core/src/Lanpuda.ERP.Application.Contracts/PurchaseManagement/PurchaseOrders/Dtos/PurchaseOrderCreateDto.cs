using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;

[Serializable]
public class PurchaseOrderCreateDto
{
    public Guid SupplierId { get; set; }

    public DateTime RequiredDate { get; set; }

    public DateTime? PromisedDate { get; set; }

    public string Contact { get; set; }

    public string ContactTel { get; set; }

    public string ShippingAddress { get; set; }

    public string Remark { get; set; }

    public List<PurchaseOrderDetailCreateDto> Details { get; set; }

    public PurchaseOrderCreateDto()
    {
        Details = new List<PurchaseOrderDetailCreateDto>();
    }
}