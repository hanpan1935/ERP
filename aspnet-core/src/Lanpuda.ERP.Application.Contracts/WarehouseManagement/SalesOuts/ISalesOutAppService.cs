using System;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

public interface ISalesOutAppService : IApplicationService
{
    Task<SalesOutDto> GetAsync(Guid id);

    Task<PagedResultDto<SalesOutDto>> GetPagedListAsync(SalesOutPagedRequestDto input);

    /// <summary>
    /// Åä»õ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(Guid id, SalesOutUpdateDto input);

    /// <summary>
    /// ³ö¿â
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task OutedAsync(Guid id);


    Task<PagedResultDto<SalesOutDetailSelectDto>> GetDetailPagedListAsync(SalesOutDetailPagedRequestDto input);


    Task<List<SalesOutDetailDto>> AutoOutAsync(Guid id);
}