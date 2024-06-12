using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories;

[Authorize]
public class SafetyInventoryAppService : ERPAppService, ISafetyInventoryAppService
{
    private readonly ISafetyInventoryRepository _repository;

    public SafetyInventoryAppService(ISafetyInventoryRepository repository)
    {
        _repository = repository;
    }

    [Authorize(ERPPermissions.SafetyInventory.Create)]
    public async Task CreateAsync(SafetyInventoryCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        SafetyInventory safetyInventory = new SafetyInventory(id);
        safetyInventory.ProductId = input.ProductId;
        safetyInventory.MinQuantity = input.MinQuantity;
        safetyInventory.MaxQuantity = input.MaxQuantity;

        var isRepeat = await CheckIsRepeatAsync(safetyInventory);
        if (isRepeat == true)
        {
            throw new UserFriendlyException("已经存在");
        }
        SafetyInventory result = await _repository.InsertAsync(safetyInventory);
    }


    [Authorize(ERPPermissions.SafetyInventory.Create)]
    public async Task BulkCreateAsync(List<SafetyInventoryCreateDto> inputs)
    {
        List<SafetyInventory> list = new List<SafetyInventory>();

        for (int i = 0; i < inputs.Count; i++)
        {
            var item = inputs[i];
            Guid id = GuidGenerator.Create();
            SafetyInventory safetyInventory = new SafetyInventory(id);
            safetyInventory.ProductId = item.ProductId;
            safetyInventory.MinQuantity = item.MinQuantity;
            safetyInventory.MaxQuantity = item.MaxQuantity;

            var isRepeat = await CheckIsRepeatAsync(safetyInventory);
            if (isRepeat == true)
            {
                throw new UserFriendlyException("第" + (i+1) +"行,存在已经创建的安全库存");
            }
            list.Add(safetyInventory);
        }
        await _repository.InsertManyAsync(list);
    }

    [Authorize(ERPPermissions.SafetyInventory.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        SafetyInventory safetyInventory = await _repository.FindAsync(id);
        if (safetyInventory == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _repository.DeleteAsync(safetyInventory);
    }


    [Authorize(ERPPermissions.SafetyInventory.Update)]
    public async Task<SafetyInventoryDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<SafetyInventory, SafetyInventoryDto>(result);
    }

    [Authorize(ERPPermissions.SafetyInventory.Default)]
    public async Task<PagedResultDto<SafetyInventoryDto>> GetPagedListAsync(SafetyInventoryPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(input.ProductId != null, m => m.ProductId == (input.ProductId))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.Product.Name.Contains(input.ProductName))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SafetyInventoryDto>(totalCount, ObjectMapper.Map<List<SafetyInventory>, List<SafetyInventoryDto>>(result));
    }

    [Authorize(ERPPermissions.SafetyInventory.Update)]
    public async Task UpdateAsync(Guid id, SafetyInventoryUpdateDto input)
    {
        SafetyInventory safetyInventory = await _repository.FindAsync(id);
        if (safetyInventory == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        var any = await _repository.AnyAsync(m=>m.ProductId == input.ProductId && m.Id != id);
        if (any)
        {
            throw new UserFriendlyException("存在已经创建的安全库存");
        }

        safetyInventory.ProductId = input.ProductId;
        safetyInventory.MinQuantity = input.MinQuantity;
        safetyInventory.MaxQuantity = input.MaxQuantity;
        var result = await _repository.UpdateAsync(safetyInventory);
    }


    private async Task<bool> CheckIsRepeatAsync(SafetyInventory safetyInventory)
    {
       return  await _repository.AnyAsync(m=>m.ProductId == safetyInventory.ProductId);
    }
}
