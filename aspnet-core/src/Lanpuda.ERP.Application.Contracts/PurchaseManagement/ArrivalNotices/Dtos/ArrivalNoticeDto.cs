using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;

[Serializable]
public class ArrivalNoticeDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string Remark { get; set; }

    public bool IsConfirmed { get; set; }

    public DateTime? ConfirmedTime { get; set; }

    public string ConfirmeUserSurname { get; set; }

    public string ConfirmeUserName { get; set; }

    public List<ArrivalNoticeDetailDto> Details { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public ArrivalNoticeDto()
    {
        Details = new List<ArrivalNoticeDetailDto>();
    }
}