using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.Customers;

public class CustomerRepository : EfCoreRepository<ERPDbContext, Customer, Guid>, ICustomerRepository
{
    public CustomerRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Customer>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}