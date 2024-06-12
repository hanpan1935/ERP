using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.Reports.Dtos.Home
{
    public class HomeWorkOrderDto : EntityDto<Guid>
    {
        public string Number { get; set; }

        public Guid? WorkshopId { get; set; }

        public string WorkshopName { get; set; }

        public Guid ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string ProductSpec { get; set; }

        public string ProductUnitName { get; set; }

        public Guid MpsId { get; set; }

        public string MpsNumber { get; set; }

        public double Quantity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        //入库数量
        public double WorkOrderStorageQuantity { get; set; }

    }
}
