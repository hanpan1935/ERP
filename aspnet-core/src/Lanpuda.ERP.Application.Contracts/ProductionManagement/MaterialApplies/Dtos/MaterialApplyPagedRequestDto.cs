using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos
{
    public class MaterialApplyPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? WorkOrderNumber { get; set; }

        public string? MpsNumber { get; set; }

        public string? ProductName { get; set; }

        public bool? IsSuccessful { get; set; }

        public bool? IsConfirmed { get; set; }
    }
}
