using System;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;

[Serializable]
public class ArrivalNoticeDetailCreateDto
{
    public Guid ArrivalNoticeId { get; set; }

    public Guid PurchaseOrderDetailId { get; set; }

    public double Quantity { get; set; }


}