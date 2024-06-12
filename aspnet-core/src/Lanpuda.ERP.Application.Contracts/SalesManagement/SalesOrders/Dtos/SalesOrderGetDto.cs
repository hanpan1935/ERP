using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos
{
    public class SalesOrderGetDto : EntityDto<Guid>
    {
        public string Number { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? PromisedDate { get; set; }

        public SalesOrderType OrderType { get; set; }

        public string Description { get; set; }

        public List<SalesOrderDetailGetDto> Details { get; set; }

        public SalesOrderGetDto()
        {
            Details = new List<SalesOrderDetailGetDto>();
        }
    }
}
