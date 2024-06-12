using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;

[Serializable]
public class PurchaseOrderDto : AuditedEntityDto<Guid>
{
    public Guid SupplierId { get; set; }

    public string SupplierShortName { get; set; }

    public string SupplierFullName { get; set; }

    public string Number { get; set; }

    public DateTime RequiredDate { get; set; }

    public DateTime? PromisedDate { get; set; }

    public PurchaseOrderType OrderType { get; set; }

    public string Contact { get; set; }

    public string ContactTel { get; set; }

    public string ShippingAddress { get; set; }

    public string Remark { get; set; }

    public bool IsConfirmed { get; set; }

    public DateTime? ConfirmedTime { get; set; }

    public string ConfirmeUserSurname { get; set; }

    public string ConfirmeUserName { get; set; }

    public PurchaseOrderCloseStatus CloseStatus { get; set; }

    public List<PurchaseOrderDetailDto> Details { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public PurchaseOrderDto()
    {
        Details = new List<PurchaseOrderDetailDto>();
    }
}