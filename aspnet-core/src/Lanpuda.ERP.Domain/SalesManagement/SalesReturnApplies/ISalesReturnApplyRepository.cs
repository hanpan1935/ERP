using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies;

public interface ISalesReturnApplyRepository : IRepository<SalesReturnApply, Guid>
{
}
