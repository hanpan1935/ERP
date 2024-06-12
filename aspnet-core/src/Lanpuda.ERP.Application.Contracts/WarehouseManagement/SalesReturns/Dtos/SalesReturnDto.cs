using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos;

[Serializable]
public class SalesReturnDto : AuditedEntityDto<Guid>
{

    public string Number { get; set; }

    public string ApplyNumber { get; set; }

    public string CustomerFullName { get; set; }
    public string CustomerShortName { get; set; }

    public SalesReturnReason Reason { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }
    public string ProductUnitName { get; set; }
    public string ProductSpec { get; set; }
    public Guid? ProductDefaultLocationId { get; set; }
    public Guid? ProductDefaultWarehouseId { get; set; }

    public double ApplyQuantity { get; set; }

    public string Batch { get; set; }
   

    public Guid? KeeperUserId { get; set; }

    public string KeeperUserSurname { get; set; }

    public string KeeperUserName { get; set; }

    public string Remark { get; set; }

    /// <summary>
    /// 入库状态  false待入库  true已入库
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// 入库时间
    /// </summary>
    public DateTime? SuccessfulTime { get; set; }


    public List<SalesReturnDetailDto> Details { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}