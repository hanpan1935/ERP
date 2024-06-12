using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;

[Serializable]
public class SalesReturnApplyDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerFullName { get; set; }
    public string CustomerShortName { get; set; }

    public SalesReturnReason Reason { get; set; }



    public bool IsProductReturn { get; set; }

    public string Description { get; set; }

    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmeTime { get; set; }
    public Guid? ConfirmeUserId { get; set; }
    public string ConfirmeUserName { get; set; }
    public string ConfirmeUserSurname { get; set; }


    public List<SalesReturnApplyDetailDto> Details { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public SalesReturnApplyDto()
    {
        Details = new List<SalesReturnApplyDetailDto>();
    }
}