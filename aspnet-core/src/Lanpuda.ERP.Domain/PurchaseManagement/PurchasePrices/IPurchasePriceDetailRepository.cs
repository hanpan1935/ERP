using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices;

public interface IPurchasePriceDetailRepository : IRepository<PurchasePriceDetail, Guid>
{
}
