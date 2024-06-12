using System;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;

[Serializable]
public class MaterialApplyDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public double StandardQuantity { get; set; }

    public string Remark { get; set; }

}