using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;

public interface IPurchaseReturnApplyDetailRepository : IRepository<PurchaseReturnApplyDetail, Guid>
{
}
