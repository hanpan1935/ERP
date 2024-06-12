using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos
{
    public class WorkOrderOutPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? MaterialApplyNumber { get; set; }

        public string? WorkOrderNumber { get; set; }

        public string? ProductName { get; set; }

        public bool? IsSuccessful { get; set; }
    }
}
