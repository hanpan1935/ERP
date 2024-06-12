using System;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;

[Serializable]
public class SalesOutDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }
}