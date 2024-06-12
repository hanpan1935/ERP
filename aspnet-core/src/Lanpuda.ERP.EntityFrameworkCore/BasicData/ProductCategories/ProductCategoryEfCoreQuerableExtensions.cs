using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.BasicData.ProductCategories;

public static class ProductCategoryEfCoreQueryableExtensions
{
    public static IQueryable<ProductCategory> IncludeDetails(this IQueryable<ProductCategory> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator) // TODO: AbpHelper generated
            ;
    }
}
