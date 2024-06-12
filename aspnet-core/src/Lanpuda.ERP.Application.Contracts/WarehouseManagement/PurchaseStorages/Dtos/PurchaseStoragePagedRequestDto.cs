using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos
{
    public class PurchaseStoragePagedRequestDto : PagedAndSortedResultRequestDto
    {

        public string? Number { get; set; }

        public string? ArrivalNoticeNumber { get; set; }

        public bool? IsSuccessful { get; set; }

        public string? SupplierName { get; set; }

        public string? ProductName { get; set; }

    }
}
