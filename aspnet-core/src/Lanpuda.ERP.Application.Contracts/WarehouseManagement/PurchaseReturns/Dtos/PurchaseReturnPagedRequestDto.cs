using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos
{
    public class PurchaseReturnPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? ApplyNumber { get; set; }

        public bool? IsSuccessful { get; set; }


        public string? SupplierName { get; set; }

        public string? ProductName { get; set; }
    }
}
