using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.Customers.Dtos
{
    public class CustomerLookupDto : EntityDto<Guid>
    {
        public string Number { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }


        /// <summary>
        /// 收货地址
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ConsigneeTel { get; set; }

    }
}
