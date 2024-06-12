using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.Workshops;

public interface IWorkshopRepository : IRepository<Workshop, Guid>
{
}
