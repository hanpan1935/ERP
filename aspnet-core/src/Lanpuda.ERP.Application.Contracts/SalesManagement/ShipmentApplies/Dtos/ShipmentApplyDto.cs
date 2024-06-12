using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;

[Serializable]
public class ShipmentApplyDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid CustomerId { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerShortName { get; set; }

    public string Address { get; set; }

    public string Consignee { get; set; }

    public string ConsigneeTel { get; set; }

    //public Guid? ApplyUserId { get; set; }
    //public string ApplyUserName { get; set; }
    //public string ApplyUserSurname { get; set; }

    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmeTime { get; set; }
    public Guid? ConfirmeUserId { get; set; }
    public string ConfirmeUserName { get; set; }
    public string ConfirmeUserSurname { get; set; }

    public List<ShipmentApplyDetailDto> Details { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public ShipmentApplyDto()
    {
        Details = new List<ShipmentApplyDetailDto>();
    }

}