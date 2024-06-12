using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.BasicData.Products;

public static class ProductEfCoreQueryableExtensions
{
    public static IQueryable<Product> IncludeDetails(this IQueryable<Product> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.ProductUnit) // TODO: AbpHelper generated
             .Include(x => x.ProductCategory)
             .Include(m => m.DefaultLocation).ThenInclude(m => m.Warehouse)
             .Include(x => x.DefaultWorkshop)
             .Include(x => x.Creator)
            ;
    }
}
