using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos
{
    public class WorkOrderMultipleCreateDto
    {
        [Required]
        public Guid MpsId { get; set; }

        public List<WorkOrderMultipleCreateDetailDto> Details { get;set; }

        public WorkOrderMultipleCreateDto()
        {
            Details = new List<WorkOrderMultipleCreateDetailDto>();
        }
    }
}
