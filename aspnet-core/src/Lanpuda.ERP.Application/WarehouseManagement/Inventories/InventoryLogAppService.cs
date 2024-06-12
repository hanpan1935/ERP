using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

[Authorize]
public class InventoryLogAppService : ERPAppService, IInventoryLogAppService
{
    private readonly IInventoryLogRepository _repository;

    public InventoryLogAppService(IInventoryLogRepository repository)
    {
        _repository = repository;
    }

    //public async Task<InventoryLogDto> GetAsync(Guid id)
    //{
    //    var result = await _repository.FindAsync(id);
    //    return ObjectMapper.Map<InventoryLog, InventoryLogDto>(result);
    //}


    [Authorize(ERPPermissions.InventoryLog.Default)]
    public async Task<PagedResultDto<InventoryLogDto>> GetPagedListAsync(InventoryLogPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.ProductId != null, m => m.ProductId.Equals(input.ProductId))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.Product.Name.Contains(input.ProductName))
            .WhereIf(input.FlowTimeStart != null, m => m.LogTime >= input.FlowTimeStart)
            .WhereIf(input.FlowTimeEnd != null, m => m.LogTime <= input.FlowTimeEnd)
            .WhereIf(input.LogType != null, m => m.LogType.Equals(input.LogType))
            .WhereIf(!string.IsNullOrEmpty(input.Batch), m => m.Batch.Contains(input.Batch))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<InventoryLogDto>(totalCount, ObjectMapper.Map<List<InventoryLog>, List<InventoryLogDto>>(result));
    }
}
