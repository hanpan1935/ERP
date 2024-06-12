using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos
{
    public class BomPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid? ProductId { get; set; }

        public string? ProductName { get; set; }

    }
}
