using System;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;

[Serializable]
public class InventoryCheckDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double InventoryQuantity { get; set; }

    /// <summary>
    /// ��ӯ�̿�
    /// </summary>
    public InventoryCheckDetailType CheckType { get; set; }

    /// <summary>
    /// ӯ������
    /// </summary>
    public double CheckQuantity { get; set; }
}