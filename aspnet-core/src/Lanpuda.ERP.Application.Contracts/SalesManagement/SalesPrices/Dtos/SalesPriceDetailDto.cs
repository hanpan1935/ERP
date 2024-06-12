using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;

[Serializable]
public class SalesPriceDetailDto : EntityDto<Guid>
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public double Price { get; set; }

    public double TaxRate { get; set; }
}