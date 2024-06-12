using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.SalesManagement.SalesPrices;

public interface ISalesPriceDetailRepository : IRepository<SalesPriceDetail, Guid>
{
}
