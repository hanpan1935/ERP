using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos
{
    public class WorkOrderOutDetailPagedRequestDto : PagedAndSortedResultRequestDto
    {

        public string WorkOrderOutNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public string MaterialApplyNumber { get; set; }
        public string ProductName { get; set; }
    }
}
