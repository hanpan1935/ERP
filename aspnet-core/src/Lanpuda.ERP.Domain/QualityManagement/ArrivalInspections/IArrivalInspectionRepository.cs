using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections;

public interface IArrivalInspectionRepository : IRepository<ArrivalInspection, Guid>
{
}
