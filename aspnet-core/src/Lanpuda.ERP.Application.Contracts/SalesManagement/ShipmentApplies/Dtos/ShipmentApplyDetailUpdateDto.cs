using System;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;

[Serializable]
public class ShipmentApplyDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid SalesOrderDetailId { get; set; }

    public double Quantity { get; set; }

}