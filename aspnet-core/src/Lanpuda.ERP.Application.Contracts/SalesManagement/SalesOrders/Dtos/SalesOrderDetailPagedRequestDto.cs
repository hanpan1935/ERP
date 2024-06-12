using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos
{
    public class SalesOrderDetailPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string SalesOrderNumber { get; set; }

        public Guid? CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string ProductName { get; set; }

        public SalesOrderType? OrderType { get; set; }

        public SalesOrderCloseStatus? CloseStatus { get; set; }

        public bool? IsConfirmed { get; set; }


        public DateTime? DeliveryDateStart { get; set; }
        public DateTime? DeliveryDateEnd { get; set; }

        public SalesOrderDetailPagedRequestDto()
        {
            this.SalesOrderNumber = string.Empty;
            this.CustomerName = string.Empty;
            this.ProductName = string.Empty;
        }

    }
}
