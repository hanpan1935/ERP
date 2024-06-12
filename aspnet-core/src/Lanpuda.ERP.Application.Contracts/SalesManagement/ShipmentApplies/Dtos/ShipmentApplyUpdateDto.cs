using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;

[Serializable]
public class ShipmentApplyUpdateDto 
{
    public Guid CustomerId { get; set; }
    /// <summary>
    /// �ջ���ַ
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// �ջ���
    /// </summary>
    public string Consignee { get; set; }

    /// <summary>
    /// �ջ��˵绰
    /// </summary>
    public string ConsigneeTel { get; set; }


    public List<ShipmentApplyDetailUpdateDto> Details { get; set; }

    public ShipmentApplyUpdateDto()
    {
        Details = new List<ShipmentApplyDetailUpdateDto>();
    }
}