using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;

[Serializable]
public class ArrivalNoticeCreateDto
{

    public DateTime ArrivalTime { get; set; }

    public string Remark { get; set; }

    public List<ArrivalNoticeDetailCreateDto> Details { get; set; }

    public ArrivalNoticeCreateDto()
    {
        Details = new List<ArrivalNoticeDetailCreateDto>();
    }
}