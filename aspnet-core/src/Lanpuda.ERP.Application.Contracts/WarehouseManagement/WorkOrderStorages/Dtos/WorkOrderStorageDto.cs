using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;

[Serializable]
public class WorkOrderStorageDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public string ApplyNumber { get; set; }

    public string WorkOrderNumber { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public Guid? ProductDefaultLocationId { get; set; }
    public Guid? ProductDefaultWarehouseId { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }

    public Guid? KeeperUserId { get; set; }

    public string KeeperUserName { get; set; }

    public string KeeperUserSurname { get; set; }

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

    public List<WorkOrderStorageDetailDto> Details { get; set; }
    
}