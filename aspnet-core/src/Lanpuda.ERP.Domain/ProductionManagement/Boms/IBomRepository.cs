using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.Boms;

public interface IBomRepository : IRepository<Bom, Guid>
{
    
}
