using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;

[Serializable]
public class SalesOrderCreateDto
{
    public Guid CustomerId { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? PromisedDate { get; set; }

    public SalesOrderType OrderType { get; set; }

    public string Description { get; set; }

    public List<SalesOrderDetailCreateDto> Details { get; set; }

    public SalesOrderCreateDto()
    {
        Details = new List<SalesOrderDetailCreateDto>();

    }
}