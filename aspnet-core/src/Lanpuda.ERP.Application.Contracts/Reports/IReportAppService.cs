using Lanpuda.ERP.Reports.Dtos.Home;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.Reports
{
    public interface IReportAppService : IApplicationService
    {
        Task<HomeDto> GetHomeDataAsync(DateTime startDate);
    }
}
