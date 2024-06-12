using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos
{
    public class SalesOutDetailSelectDto : EntityDto<Guid>
    {
        public string SalesOutNumber { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerShortName { get; set; }

        public string ProductName { get; set; }

        public string Batch { get; set;}

        public double OutQuantity { get; set; }


        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime SuccessfulTime { get; set; }

    }
}
