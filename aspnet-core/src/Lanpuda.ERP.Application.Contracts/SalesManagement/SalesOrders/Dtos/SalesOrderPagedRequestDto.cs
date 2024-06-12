using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos
{
    public class SalesOrderPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public Guid? CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public DateTime? RequiredDateStart { get; set; }
        public DateTime? RequiredDateEnd { get; set; }


        public DateTime? PromisedDateStart { get; set; }
        public DateTime? PromisedDateEnd { get; set; }


        public SalesOrderType? OrderType { get; set; }

        public bool? IsConfirmed { get; set; }

        public SalesOrderCloseStatus? CloseStatus { get; set; }
    }
}
