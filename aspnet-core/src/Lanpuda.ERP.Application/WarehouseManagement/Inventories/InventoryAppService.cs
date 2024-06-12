using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

[Authorize]
public class InventoryAppService : ERPAppService, IInventoryAppService
{
    private readonly IInventoryRepository _repository;

    public InventoryAppService(IInventoryRepository repository)
    {
        _repository = repository;
    }


    [Authorize(ERPPermissions.Inventory.Default)]
    public async Task<InventoryPagedResultDto> GetPagedListAsync(InventoryPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(input.WarehouseId != null, m => m.Location.WarehouseId.Equals(input.WarehouseId))
            .WhereIf(input.LocationId != null, m => m.LocationId.Equals(input.LocationId))
            .WhereIf(input.ProductId != null, m => m.ProductId.Equals(input.ProductId))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.Product.Name.Contains(input.ProductName))
            .WhereIf(!string.IsNullOrEmpty(input.ProductSpec), m => m.Product.Spec.Contains(input.ProductSpec))
            .WhereIf(!string.IsNullOrEmpty(input.Batch), m => m.Batch.Contains(input.Batch))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        double totalQuantity = await AsyncExecuter.SumAsync(query,m=>m.Quantity);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var list = await AsyncExecuter.ToListAsync(query);

        var result = new InventoryPagedResultDto(totalCount, ObjectMapper.Map<List<Inventory>, List<InventoryDto>>(list));
        result.TotalQuantity = totalQuantity;
        return result;
    }

    //public async Task<List<InventoryDto>> GetListAsync(InventoryListRequestDto input)
    //{
    //    var query = await _repository.GetQueryableAsync();
    //    query = query
    //        .WhereIf(input.ProductId != null, m => m.LocationId.Equals(input.ProductId))
    //        .WhereIf(string.IsNullOrEmpty(input.Batch), m => m.Batch.Contains(input.Batch))
    //        .WhereIf(input.WarehourseId != null, m => m.Location.WarehouseId.Equals(input.WarehourseId))
    //        .WhereIf(input.LocationId != null, m => m.LocationId.Equals(input.LocationId))
    //        ;

    //    var resultList = await AsyncExecuter.ToListAsync(query);
    //    return ObjectMapper.Map<List<Inventory>, List<InventoryDto>>(resultList);
    //}
}
