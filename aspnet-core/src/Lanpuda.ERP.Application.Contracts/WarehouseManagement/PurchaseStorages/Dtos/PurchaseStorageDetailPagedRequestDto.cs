using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos
{
    public class PurchaseStorageDetailPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string PurchaseStorageNumber { get; set; }

        public string ProductName { get; set; }

        public string Batch { get; set; }

        public string SupplierName { get; set; }
    }
}
