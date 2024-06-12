using System;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;

[Serializable]
public class WorkOrderReturnDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid LocationId { get; set; }

    /// <summary>
    /// �������
    /// </summary>
    public double Quantity { get; set; }
}