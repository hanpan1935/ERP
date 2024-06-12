using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;

public interface IMaterialReturnApplyRepository : IRepository<MaterialReturnApply, Guid>
{
}
