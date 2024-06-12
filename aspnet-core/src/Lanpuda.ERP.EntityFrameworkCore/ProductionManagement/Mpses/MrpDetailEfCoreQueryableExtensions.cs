using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.ProductionManagement.Mpses
{
    public static class MrpDetailEfCoreQueryableExtensions
    {

        public static IQueryable<MrpDetail> IncludeDetails(this IQueryable<MrpDetail> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                   .Include(m => m.Product).ThenInclude(m => m.ProductUnit)
                   ;
        }
    }
}
