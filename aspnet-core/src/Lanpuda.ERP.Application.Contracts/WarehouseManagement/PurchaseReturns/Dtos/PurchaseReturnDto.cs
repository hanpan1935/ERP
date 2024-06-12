using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;

[Serializable]
public class PurchaseReturnDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }


    public Guid PurchaseReturnApplyDetailId { get; set; }
    public string PurchaseReturnApplyNumber { get; set; }

    public PurchaseReturnReason ReturnReason { get; set; }

    public string PurchaseReturnApplyDescription { get; set; }


    public Guid SupplierId { get; set; }
    public string SupplierFullName { get; set; }
    public string SupplierShortName { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductUnitName { get; set; }
    public string ProductSpec { get; set; }

    public double ApplyQuantity { get; set; }

    public string Batch { get; set; }

    public Guid? KeeperUserId { get; set; }

    public string KeeperUserName { get; set; }

    public string KeeperUserSurname { get; set; }

    public string Remark { get; set; }


    /// <summary>
    /// 是否入库
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// 入库时间
    /// </summary>
    public DateTime? SuccessfulTime { get; set; }


    public List<PurchaseReturnDetailDto> Details { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public PurchaseReturnDto()
    {
        Details = new List<PurchaseReturnDetailDto>();
    }
}