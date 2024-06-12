using System;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;

[Serializable]
public class ShipmentApplyDetailCreateDto
{
    public Guid SalesOrderDetailId { get; set; }

    public double Quantity { get; set; }

}