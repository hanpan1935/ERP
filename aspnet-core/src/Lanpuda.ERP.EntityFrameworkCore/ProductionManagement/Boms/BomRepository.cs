using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Boms;

public class BomRepository : EfCoreRepository<ERPDbContext, Bom, Guid>, IBomRepository
{
    public BomRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Bom>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

   
}