using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos
{
    public class WorkOrderStorageApplyPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? WorkOrderNumber { get; set; }

        public bool? IsConfirmed { get; set; }

    }
}
