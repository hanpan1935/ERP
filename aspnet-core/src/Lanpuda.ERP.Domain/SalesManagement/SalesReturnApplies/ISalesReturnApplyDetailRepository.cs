using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies;

public interface ISalesReturnApplyDetailRepository : IRepository<SalesReturnApplyDetail, Guid>
{
}
