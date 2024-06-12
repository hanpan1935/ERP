using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos
{
    public class ShipmentApplyPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? CustomerName { get; set; }

        public bool? IsConfirmed { get; set; }

    }
}
