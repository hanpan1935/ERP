using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos
{
    public class MpsPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? ProductName { get; set; }

        public MpsType? MpsType { get; set; }

        public DateTime? StartDateStart { get; set; }

        public DateTime? StartDateEnd { get; set; }

        public DateTime? CompleteDateStart { get; set; }

        public DateTime? CompleteDateEnd { get; set; }


        public bool? IsConfirmed { get; set; }

    }
}
