using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos
{
    public class WorkOrderPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public string? MpsNumber { get; set; }

        public string? ProductName { get; set; }

        public Guid? WorkshopId { get; set; }

        public bool? IsConfirmed { get; set; }

        /// <summary>
        /// 开工时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompletionDate { get; set; }
    }
}
