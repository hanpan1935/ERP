using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;

[Serializable]
public class ShipmentApplyCreateDto
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


    public List<ShipmentApplyDetailCreateDto> Details { get; set; }

    public ShipmentApplyCreateDto()
    {
        Details = new List<ShipmentApplyDetailCreateDto>();
    }
}