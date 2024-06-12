using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;

[Serializable]
public class SalesReturnApplyDetailDto : EntityDto<Guid>
{
    public Guid SalesReturnApplyId { get; set; }

    public Guid SalesOutDetailId { get; set; }

    public double Quantity { get; set; }

    public string Description { get; set; }
    ///

    public string Batch { get; set; }

    public double OutQuantity { get; set; }



    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductUnitName { get; set; }

    public string ProductSpec { get; set; }

   
}