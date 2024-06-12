using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages;

public interface IOtherStorageDetailRepository : IRepository<OtherStorageDetail, Guid>
{
}
