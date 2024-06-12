using System;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;

[Serializable]
public class PurchaseOrderDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid PurchaseOrderId { get; set; }

    public Guid ProductId { get; set; }

    public DateTime PromiseDate { get; set; }

    public double Quantity { get; set; }

    public double Price { get; set; }

    public double TaxRate { get; set; }

    public string Remark { get; set; }

}