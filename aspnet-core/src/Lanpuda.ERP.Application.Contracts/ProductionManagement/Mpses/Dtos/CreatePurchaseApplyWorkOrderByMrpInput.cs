using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos
{
    public class CreatePurchaseApplyWorkOrderByMrpInput
    {
        public Guid MpsId { get; set; }

        public List<Guid> MrpDetailIdList { get; set; }


        

        public CreatePurchaseApplyWorkOrderByMrpInput()
        {
            MrpDetailIdList = new List<Guid>();
        }
    }
}
