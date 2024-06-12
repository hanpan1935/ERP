using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies;

public interface IPurchaseApplyRepository : IRepository<PurchaseApply, Guid>
{
}
