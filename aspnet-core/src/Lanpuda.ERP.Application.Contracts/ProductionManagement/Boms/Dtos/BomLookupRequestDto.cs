using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.Boms.Dtos
{
    public class BomLookupRequestDto
    {
        public Guid ProductId { get; set; }
       
        public string? ProductName { get; set; }
       
        public bool? IsActive { get; set; }

        public double Quantity { get; set; }
    }
}
