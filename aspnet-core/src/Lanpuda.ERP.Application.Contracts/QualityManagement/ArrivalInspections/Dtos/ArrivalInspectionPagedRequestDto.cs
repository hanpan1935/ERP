using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos
{
    public class ArrivalInspectionPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? ArrivalNoticeNumber { get; set; }

        public string? PurchaseStorageNumber { get; set; }

        public bool? IsConfirmed { get; set; }

        public string? ProductName { get; set; }
    }
}
