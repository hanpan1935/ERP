using System;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos;

[Serializable]
public class SalesReturnDetailUpdateDto
{
    public Guid? Id { get; set; }

    //public Guid ProductId { get; set; }

    public Guid LocationId { get; set; }

   // public string Batch { get; set; }

    public double Quantity { get; set; }

}