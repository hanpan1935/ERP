using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;

[Serializable]
public class PurchasePriceDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }
    public Guid SupplierId { get; set; }

    public string SupplierShortName { get; set; }

    public string SupplierFullName { get; set; }

    public int AvgDeliveryDate { get; set; }

    public DateTime QuotationDate { get; set; }

    public string Remark { get; set; }

    public List<PurchasePriceDetailDto> Details { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public PurchasePriceDto()
    {
        Details = new List<PurchasePriceDetailDto>();
    }
}