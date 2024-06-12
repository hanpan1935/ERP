using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos
{
    public class MaterialReturnApplyPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        //public string WorkOrderNumber { get; set; }

        public bool? IsConfirmed { get; set; }

    }
}
