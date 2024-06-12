using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;

[Serializable]
public class WorkOrderReturnDetailDto : EntityDto<Guid>
{
    

    public Guid WarehouseId { get; set; }

    public string WarehouseName { get; set; }

    public Guid LocationId { get; set; }

    public string LocationName { get; set; }


    public double Quantity { get; set; }

}