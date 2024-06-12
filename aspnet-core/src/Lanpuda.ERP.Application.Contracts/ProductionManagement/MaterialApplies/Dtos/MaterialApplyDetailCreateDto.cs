using System;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;

[Serializable]
public class MaterialApplyDetailCreateDto
{
   

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public double StandardQuantity { get; set; }

    public string Remark { get; set; }
}