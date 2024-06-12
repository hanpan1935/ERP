using System;
using Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns;

public interface ISalesReturnAppService :IApplicationService
{
    Task<SalesReturnDto> GetAsync(Guid id);


    Task<PagedResultDto<SalesReturnDto>> GetPagedListAsync(SalesReturnPagedRequestDto input);


    /// <summary>
    /// Ñ¡Ôñ¿âÎ»
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(Guid id, SalesReturnUpdateDto input);

    /// <summary>
    /// Èë¿â
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task StoragedAsync(Guid id);
}