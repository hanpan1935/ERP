using System;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;

[Serializable]
public class InventoryCheckDetailCreateDto
{
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

}