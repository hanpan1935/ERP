using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;

public interface IMaterialReturnApplyDetailRepository : IRepository<MaterialReturnApplyDetail, Guid>
{
}
