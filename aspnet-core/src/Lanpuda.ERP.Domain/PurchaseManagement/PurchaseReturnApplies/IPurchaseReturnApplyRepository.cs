using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;

public interface IPurchaseReturnApplyRepository : IRepository<PurchaseReturnApply, Guid>
{
}
