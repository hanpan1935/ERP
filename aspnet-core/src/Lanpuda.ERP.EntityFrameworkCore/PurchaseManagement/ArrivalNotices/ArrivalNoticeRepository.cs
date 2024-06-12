using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices;

public class ArrivalNoticeRepository : EfCoreRepository<ERPDbContext, ArrivalNotice, Guid>, IArrivalNoticeRepository
{
    public ArrivalNoticeRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<ArrivalNotice>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}