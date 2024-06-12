using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Locations.Dtos
{
    public class LocationPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid? WarehouseId { get; set; }

        public string? Number { get; set; }

        public string? Name { get; set; }

    }
}
