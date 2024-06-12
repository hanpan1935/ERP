using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos
{
    public class SalesOutPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? ShipmentApplyNumber { get; set; }

        public string? Number { get; set; }


        public string? CustomerName { get; set; }

        public bool? IsSuccessful { get; set; }


        public DateTime? OutTimeStart { get; set; }

        public DateTime? OutTimeEnd { get; set; }

        public Guid? KeeperUserId { get; set; }

      
    }
}
