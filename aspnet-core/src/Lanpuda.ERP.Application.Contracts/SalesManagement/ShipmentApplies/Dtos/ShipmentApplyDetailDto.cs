using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;

[Serializable]
public class ShipmentApplyDetailDto : EntityDto<Guid>
{
    public Guid SalesOrderDetailId { get; set; }

    public string SalesOrderNumber { get; set; }


    public double Quantity { get; set; }
   
    /// <summary>
    /// 
    /// </summary>
    public double OrderQuantity { get; set; }

    public DateTime DeliveryDate { get; set; }

    public string Requirement { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductUnitName { get; set; }

    public string ProductSpec { get; set; }
}