using Lanpuda.ERP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Mpses
{
    public class MrpDetailRepository : EfCoreRepository<ERPDbContext, MrpDetail, Guid>, IMrpDetailRepository
    {
        public MrpDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<MrpDetail>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}