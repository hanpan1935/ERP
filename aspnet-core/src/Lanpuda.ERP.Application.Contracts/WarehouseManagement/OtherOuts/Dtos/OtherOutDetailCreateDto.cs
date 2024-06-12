using System;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;

[Serializable]
public class OtherOutDetailCreateDto
{
    public Guid OtherOutId { get; set; }

    public Guid ProductId { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }
}