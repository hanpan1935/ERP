using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.SalesManagement.Customers;

public interface ICustomerRepository : IRepository<Customer, Guid>
{
}
