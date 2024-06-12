using System;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;

[Serializable]
public class ArrivalNoticeDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid ArrivalNoticeId { get; set; }

    public Guid PurchaseOrderDetailId { get; set; }

    public double Quantity { get; set; }

}