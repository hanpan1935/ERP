using System;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;

[Serializable]
public class OtherStorageDetailCreateDto
{
    public Guid OtherStorageId { get; set; }

    public Guid ProductId { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

    public double Price { get; set; }

    public int Sort { get; set; }
    
}