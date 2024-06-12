using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.BasicData.ProductCategories
{
    /// <summary>
    /// 产品分类
    /// </summary>
    public class ProductCategory : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public string Remark { get; set; }


        public IdentityUser Creator { get; set; }


        protected ProductCategory()
        {
        }

        public ProductCategory(
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
