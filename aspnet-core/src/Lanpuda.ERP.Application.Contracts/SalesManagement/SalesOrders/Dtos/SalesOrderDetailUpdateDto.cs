using System;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;

[Serializable]
public class SalesOrderDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid SalesOrderId { get; set; }

    public DateTime DeliveryDate { get; set; }

    public double Quantity { get; set; }

    public Guid ProductId { get; set; }

    public double Price { get; set; }

    public double TaxRate { get; set; }

    public string Requirement { get; set; }
}