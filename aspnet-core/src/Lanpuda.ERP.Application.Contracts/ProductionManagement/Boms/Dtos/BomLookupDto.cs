﻿using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos
{
    public class BomLookupDto 
    {
        public Guid? ParentId { get; set; }

        public Guid? BomDetailId { get; set; }

        public Guid ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string ProductSpec { get; set; }

        public string ProductUnitName { get; set; }

        public ProductSourceType ProductSourceType { get; set; }

        public int LeadTime { get; set; }   

        public double ProductionQuantity { get; set; }

        public double Quantity { get; set; }
    }
}
