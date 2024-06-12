using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;

[Serializable]
public class WorkOrderReturnDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public string MaterialReturnApplyNumber { get; set; }

    public string WorkOrderNumber { get; set; }

    public Guid ProductId { get; set; }
    public Guid? ProductDefaultLocationId { get; set; }
    public Guid? ProductDefaultWarehouseId { get; set; }

    public string ProductName { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public double ApplyQuantity { get; set; }

    public string Batch { get; set; }

    public Guid? KeeperUserId { get; set; }

    public string KeeperUserSurname { get; set; }


    public string KeeperUserName { get; set; }

    public string Remark { get; set; }

    public bool IsSuccessful { get; set; }
    /// <summary>
    /// »Îø‚ ±º‰
    /// </summary>
    public DateTime? SuccessfulTime { get; set; }

    public List<WorkOrderReturnDetailDto> Details { get; set; }



    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}