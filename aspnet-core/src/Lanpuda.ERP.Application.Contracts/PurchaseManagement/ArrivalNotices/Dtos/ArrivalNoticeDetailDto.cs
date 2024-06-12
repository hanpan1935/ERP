using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;

[Serializable]
public class ArrivalNoticeDetailDto : EntityDto<Guid>
{
    public Guid ArrivalNoticeId { get; set; }

    public Guid PurchaseOrderDetailId { get; set; }

    public double Quantity { get; set; }

 


    #region PurchaseOrderDetail

    public string PurchaseOrderNumber { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public DateTime PurchaseOrderDetailPromiseDate { get; set; }

    public double PurchaseOrderDetailQuantity { get; set; }

    public decimal PurchaseOrderDetailPrice { get; set; }

    public decimal PurchaseOrderDetailTaxRate { get; set; }

    #endregion

    #region Product
    public Guid? DefaultLocationId { get; set; }

    public Guid DefaultWarehouseId { get; set; }


    #endregion
}