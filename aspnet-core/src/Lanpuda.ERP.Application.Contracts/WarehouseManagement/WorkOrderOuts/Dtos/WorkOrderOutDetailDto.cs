using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;

[Serializable]
public class WorkOrderOutDetailDto : EntityDto<Guid>
{
    public Guid WorkOrderOutId { get; set; }
   

    public string WarehouseName { get; set; }

    public string LocationName { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }
 
}