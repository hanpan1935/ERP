using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;

[Serializable]
public class MaterialApplyDetailDto : EntityDto<Guid>
{
    public Guid MaterialApplyId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductNumber { get; set; }

    public string ProductName { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }


    public double Quantity { get; set; }

    public double StandardQuantity { get; set; }

    public string Remark { get; set; }

}