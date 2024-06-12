using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos
{
    [Serializable]
    public class MrpDetailDto : AuditedEntityDto<Guid>
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductNumber { get; set; }

        public string ProductSpec { get; set; }

        public string ProductUnitName { get; set; }

        public ProductSourceType ProductSourceType { get; set; }

        public int ProductLeadTime { get; set; }

        public DateTime RequiredDate { get; set; }

        public double Quantity { get; set; }
    }
}
