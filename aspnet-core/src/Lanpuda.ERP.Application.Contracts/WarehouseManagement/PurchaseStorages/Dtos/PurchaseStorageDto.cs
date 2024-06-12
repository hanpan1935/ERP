using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;

[Serializable]
public class PurchaseStorageDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid ArrivalNoticeDetailId { get; set; }

    public string ArrivalNoticeNumber { get; set; }


    public string SupplierNumber { get; set; }
    public string SupplierShortName { get; set; }
    public string SupplierFullName { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductUnitName { get; set; }
    public string ProductSpec { get; set; }
    public Guid? ProductDefaultWarehouseId { get; set; }
    public Guid? ProductDefaultLocationId { get; set; }


    public double ApplyQuantity { get; set; }


    /// <summary>
    /// 入库人
    /// </summary>
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


    public List<PurchaseStorageDetailDto> Details { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }


    public PurchaseStorageDto()
    {
    }
}