using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos
{
    public class ArrivalNoticePagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public bool? IsConfirmed { get; set; }

        public DateTime? ArrivalTimeStart { get; set; }

        public DateTime? ArrivalTimeEnd { get; set; }
    }
}
