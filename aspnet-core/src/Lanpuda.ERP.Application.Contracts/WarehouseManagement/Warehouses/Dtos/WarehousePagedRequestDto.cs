using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos
{
    public class WarehousePagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }


        public string? Name { get; set; }
    }
}
