using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;

[Serializable]
public class WorkOrderOutDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }


    public string MaterialApplyNumber { get; set; }

    public string WorkOrderNumber { get; set; }


    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductUnitName { get; set; }
    public string ProductSpec { get; set; }

    public double ApplyQuantity { get; set; }

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
   


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public List<WorkOrderOutDetailDto> Details { get; set; }


}