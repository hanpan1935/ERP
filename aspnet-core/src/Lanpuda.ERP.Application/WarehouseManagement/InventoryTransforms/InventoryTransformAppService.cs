using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;
using Lanpuda.ERP.WarehouseManagement.InventoryTransforms;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms;

[Authorize]
public class InventoryTransformAppService : ERPAppService, IInventoryTransformAppService
{
    private readonly IInventoryTransformRepository _inventoryTransformRepository;
    private readonly IInventoryTransformAfterDetailRepository _inventoryTransformDetailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;


    public InventoryTransformAppService(
        IInventoryTransformRepository repository,
        IInventoryTransformAfterDetailRepository inventoryTransformDetailRepository,
        IUniqueCodeUtils uniqueCodeUtils,
         IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository
        )
    {
        _inventoryTransformRepository = repository;
        _inventoryTransformDetailRepository = inventoryTransformDetailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
    }



    [Authorize(ERPPermissions.InventoryTransform.Create)]
    public async Task CreateAsync(InventoryTransformCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.InventoryTransformPrefix); ;
        InventoryTransform inventoryTransform = new InventoryTransform(id);
        inventoryTransform.Number = number;
        inventoryTransform.Reason = input.Reason;

        InventoryTransform result = await _inventoryTransformRepository.InsertAsync(inventoryTransform);
        //await _inventoryTransformDetailRepository.InsertManyAsync(details);
    }



    [Authorize(ERPPermissions.InventoryTransform.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        InventoryTransform inventoryTransform = await _inventoryTransformRepository.FindAsync(id);
        if (inventoryTransform == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        if (inventoryTransform.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经转换,无法删除");
        }

        await _inventoryTransformRepository.DeleteAsync(inventoryTransform);
    }



    [Authorize(ERPPermissions.InventoryTransform.Update)]
    public async Task<InventoryTransformDto> GetAsync(Guid id)
    {
        var entity = await _inventoryTransformRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<InventoryTransform, InventoryTransformDto>(entity);
        return dto;
    }



    [Authorize(ERPPermissions.InventoryTransform.Default)]
    public async Task<PagedResultDto<InventoryTransformDto>> GetPagedListAsync(InventoryTransformPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inventoryTransformRepository.WithDetailsAsync();
        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.KeeperUserId != null, x => x.KeeperUserId == input.KeeperUserId)
            .WhereIf(input.IsSuccessful != null, x => x.IsSuccessful == input.IsSuccessful)
            .WhereIf(input.SuccessfulTime != null, x => x.SuccessfulTime == input.SuccessfulTime)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<InventoryTransformDto>(totalCount, ObjectMapper.Map<List<InventoryTransform>, List<InventoryTransformDto>>(result));
    }



    [Authorize(ERPPermissions.InventoryTransform.Confirm)]
    public async Task ConfirmedAsync(Guid id)
    {
        InventoryTransform inventoryTransform = await _inventoryTransformRepository.GetAsync(id, true);
        if (inventoryTransform == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (inventoryTransform.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库了，不要重复入库");
        }

        //行项目部能为空
        if (inventoryTransform.BeforeDetails == null || inventoryTransform.BeforeDetails.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        if (inventoryTransform.AfterDetails == null || inventoryTransform.AfterDetails.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }


        foreach (var item in inventoryTransform.BeforeDetails)
        {
            if (item.Quantity < 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }

        foreach (var item in inventoryTransform.AfterDetails)
        {
            if (item.Quantity < 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }


        inventoryTransform.IsSuccessful = true;
        inventoryTransform.SuccessfulTime = Clock.Now;
        inventoryTransform.KeeperUserId = CurrentUser.Id;

        var dto = ObjectMapper.Map<InventoryTransform, InventoryTransformDto>(inventoryTransform);
    }



    [Authorize(ERPPermissions.InventoryTransform.Update)]
    public async Task UpdateAsync(Guid id, InventoryTransformUpdateDto input)
    {
        InventoryTransform inventoryTransform = await _inventoryTransformRepository.FindAsync(id);
        if (inventoryTransform == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (inventoryTransform.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经转换！无法编辑");
        }
        inventoryTransform.Reason = input.Reason;


    }
}
