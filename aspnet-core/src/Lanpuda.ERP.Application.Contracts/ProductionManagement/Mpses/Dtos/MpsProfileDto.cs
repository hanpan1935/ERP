using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos
{
    [Serializable]
    public class MpsProfileDto : AuditedEntityDto<Guid>
    {
        public string Number { get; set; }

        public MpsType MpsType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CompleteDate { get; set; }

        public Guid ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string ProductSpec { get; set; }

        public string ProductUnitName { get; set; }

        public double Quantity { get; set; }


        public string Remark { get; set; }

        public bool IsConfirmed { get; set; }

        public string ConfirmedUserSurname { get; set; }

        public string ConfirmedUserName { get; set; }

        public DateTime? ConfirmedTime { get; set; }

        public List<MpsDetailDto> Details { get; set; }
        public List<MrpDetailDto> MrpDetails { get; set; }
        public List<WorkOrderDto> WorkOrderDetails { get; set; }

        public Guid? PurchaseApplyId { get; set; }
        public string PurchaseApplyNumber { get; set; }

        public string CreatorSurname { get; set; }

        public string CreatorName { get; set; }
        public MpsProfileDto()
        {
            Details = new List<MpsDetailDto>();
            MrpDetails = new List<MrpDetailDto>();
        }
    }
}
