using Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;

[Serializable]
public class SalesOutDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }



    public string Remark { get; set; }

    public Guid ShipmentApplyDetailId { get; set; }


    public string ShipmentApplyNumber { get; set; }
    public string ShipmentApplyAddress { get; set; }
    public string ShipmentApplyConsignee { get; set; }
    public string ShipmentApplyConsigneeTel { get; set; }


    public string CustomerNumber { get; set; }
    public string CustomerShortName { get; set; }
    public string CustomerFullName { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductUnitName { get; set; }
    public string ProductSpec { get; set; }

    public double ApplyQuantity { get; set; }



    public Guid? KeeperUserId { get; set; }
    public string KeeperUserSurname { get; set; }
    public string KeeperUserName { get; set; }
    /// <summary>
    /// 出库状态  
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// 出库时间
    /// </summary>
    public DateTime? SuccessfulTime { get; set; }


    public List<SalesOutDetailDto> Details { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public SalesOutDto()
    {
        Details = new List<SalesOutDetailDto>();
    }
}