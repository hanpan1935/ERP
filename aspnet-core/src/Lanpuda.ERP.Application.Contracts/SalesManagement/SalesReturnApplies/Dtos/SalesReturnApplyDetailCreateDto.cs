using System;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;

[Serializable]
public class SalesReturnApplyDetailCreateDto
{
    public Guid SalesOutDetailId { get; set; }

    public double Quantity { get; set; }

    public string Description { get; set; }

}