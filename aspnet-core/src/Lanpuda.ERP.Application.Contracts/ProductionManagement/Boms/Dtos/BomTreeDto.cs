using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos
{
    public class BomTreeDto
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public Guid ProductId { get; set; } 

        public string ProductName { get; set; }

        public string ProductNumber { get; set; }

        public string ProductSpec { get; set; }

        public string ProductUnitName { get; set; }

        public double Quantity { get; set; }

        public ProductSourceType ProductSourceType { get; set; }
    }
}
