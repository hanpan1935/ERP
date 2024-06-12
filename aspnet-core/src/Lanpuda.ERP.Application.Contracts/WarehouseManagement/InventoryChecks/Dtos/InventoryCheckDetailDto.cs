using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;

[Serializable]
public class InventoryCheckDetailDto : EntityDto<Guid>
{
    public Guid InventoryCheckId { get; set; }

    public Guid ProductId { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double InventoryQuantity { get; set; }

    /// <summary>
    /// ÅÌÓ¯ÅÌ¿÷
    /// </summary>
    public InventoryCheckDetailType CheckType { get; set; }

    /// <summary>
    /// Ó¯¿÷ÊýÁ¿
    /// </summary>
    public double CheckQuantity { get; set; }




    public string ProductName { get; set; }
    public string WarehouseName { get;set; }
    public string LocationName { get; set; }
}