using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;

[Serializable]
public class SalesOutDetailDto : EntityDto<Guid>
{
    public Guid SalesOutId { get; set; }

    public Guid ProductId { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

    public string ProductName { get; set; }

    public string WarehouseName { get; set; }

    public string LocationName { get; set; }

}