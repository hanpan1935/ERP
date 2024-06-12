using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections;

public interface IProcessInspectionRepository : IRepository<ProcessInspection, Guid>
{
}
