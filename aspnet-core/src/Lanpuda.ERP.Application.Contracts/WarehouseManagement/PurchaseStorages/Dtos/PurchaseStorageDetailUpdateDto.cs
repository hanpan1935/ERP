using System;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;

[Serializable]
public class PurchaseStorageDetailUpdateDto
{
    public Guid? Id { get; set; }


    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

}