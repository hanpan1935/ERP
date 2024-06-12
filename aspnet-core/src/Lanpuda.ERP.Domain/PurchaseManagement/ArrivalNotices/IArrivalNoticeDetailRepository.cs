using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices;

public interface IArrivalNoticeDetailRepository : IRepository<ArrivalNoticeDetail, Guid>
{
}
