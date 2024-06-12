using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos
{
    public class SupplierLookupDto : EntityDto<Guid>
    {
        public string Number { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }
    }
}
