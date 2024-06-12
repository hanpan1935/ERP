using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;

[Serializable]
public class ArrivalNoticeUpdateDto
{
    public DateTime ArrivalTime { get; set; }

    public string Remark { get; set; }

    public List<ArrivalNoticeDetailUpdateDto> Details { get; set; }

    public ArrivalNoticeUpdateDto()
    {
        Details = new List<ArrivalNoticeDetailUpdateDto>();
    }
}