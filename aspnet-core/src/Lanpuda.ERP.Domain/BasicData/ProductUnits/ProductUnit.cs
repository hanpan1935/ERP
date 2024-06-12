using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.BasicData.ProductUnits
{
    /// <summary>
    /// 产品单位
    /// </summary>
    public class ProductUnit : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        
        public string Number { get; set; }

        public string Remark { get; set; }

        public IdentityUser Creator { get; set; }

        protected ProductUnit()
        {
        }

        public ProductUnit(
            Guid id,
            string name,
            string number,
            string remark
        ) : base(id)
        {
            Name = name;
            Number = number;
            Remark = remark;
        }
    }
}
