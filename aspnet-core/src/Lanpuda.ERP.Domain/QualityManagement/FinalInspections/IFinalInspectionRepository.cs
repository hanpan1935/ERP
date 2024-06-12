using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.QualityManagement.FinalInspections;

public interface IFinalInspectionRepository : IRepository<FinalInspection, Guid>
{
}
