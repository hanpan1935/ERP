using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies;

public interface IMaterialApplyDetailRepository : IRepository<MaterialApplyDetail, Guid>
{
}
