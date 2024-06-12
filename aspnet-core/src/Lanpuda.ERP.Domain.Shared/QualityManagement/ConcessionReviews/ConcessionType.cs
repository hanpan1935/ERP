using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.ERP.QualityManagement.ConcessionReviews
{
    public enum ConcessionType
    {
        [Display(Name = "来料检验", Description = "来料检验", Order = 1)]
        ArrivalInspection = 0,

        [Display(Name = "过程检验", Description = "过程检验", Order = 2)]
        ProcessInspection = 1,

        [Display(Name = "产品终检", Description = "产品终检", Order = 3)]
        FinalInspection = 2,

        [Display(Name = "不良评审", Description = "不良评审", Order = 4)]
        BadReview = 3,
    }
}
