using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;

[Serializable]
public class SalesPriceDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerFullName { get; set; }

    public string CustomerShortName { get; set; }

    public DateTime ValidDate { get; set; }

    //public string ManagerUserSurname { get; set; }

    //public string ManagerUserName { get; set; }

    public string Remark { get; set; }

    public List<SalesPriceDetailDto> Details { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}