using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos
{
    public class WorkOrderAutoCreateResultDto
    {
        public List<WorkOrderAutoCreateDto> WorkOrderList { get; set; }

        public List<BomLookupDto> BomDetailList { get; set; }
    }
}
