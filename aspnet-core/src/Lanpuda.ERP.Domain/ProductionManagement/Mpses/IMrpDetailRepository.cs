using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.Mpses
{
    public interface IMrpDetailRepository : IRepository<MrpDetail, Guid>
    {
    }
}
