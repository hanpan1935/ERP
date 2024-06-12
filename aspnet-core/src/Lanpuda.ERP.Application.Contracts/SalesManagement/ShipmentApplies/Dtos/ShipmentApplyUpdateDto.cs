using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;

[Serializable]
public class ShipmentApplyUpdateDto 
{
    public Guid CustomerId { get; set; }
    /// <summary>
    /// 收货地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 收货人
    /// </summary>
    public string Consignee { get; set; }

    /// <summary>
    /// 收货人电话
    /// </summary>
    public string ConsigneeTel { get; set; }


    public List<ShipmentApplyDetailUpdateDto> Details { get; set; }

    public ShipmentApplyUpdateDto()
    {
        Details = new List<ShipmentApplyDetailUpdateDto>();
    }
}