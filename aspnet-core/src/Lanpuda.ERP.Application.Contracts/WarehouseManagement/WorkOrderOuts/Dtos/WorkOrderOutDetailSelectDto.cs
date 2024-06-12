using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos
{
    public class WorkOrderOutDetailSelectDto :  AuditedEntityDto<Guid>
    {
        public Guid WorkOrderOutId { get; set; }

        public string WorkOrderOutNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public string MaterialApplyNumber { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductNumber { get; set; }

        public string ProductSpec { get; set; }

        public string ProductUnitName { get; set; }

        public string WarehouseName { get; set; }

        public string LocationName { get; set; }

        public Guid LocationId { get; set; }

        public string Batch { get; set; }

        public double Quantity { get; set; }

    }
}
