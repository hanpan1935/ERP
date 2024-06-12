using System;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos;

[Serializable]
public class BomDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid BomId { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }
}