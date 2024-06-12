using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;

[Serializable]
public class SalesOrderDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerShortName { get; set; }

    public string CustomerFullName { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? PromisedDate { get; set; }

    public SalesOrderType OrderType { get; set; }

    public string Description { get; set; }

    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmeTime { get; set; }
    public Guid? ConfirmeUserId { get; set; }
    public string ConfirmeUserSurname { get; set; }
    public string ConfirmeUserName { get; set; }

    public SalesOrderCloseStatus CloseStatus { get; set; }

    public List<SalesOrderDetailDto> Details { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public SalesOrderDto()
    {
        Details = new List<SalesOrderDetailDto>();
    }
}