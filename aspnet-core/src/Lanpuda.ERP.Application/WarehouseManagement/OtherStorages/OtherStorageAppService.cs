using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Entities;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Volo.Abp.Domain.Repositories;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages;

[Authorize]
public class OtherStorageAppService :ERPAppService, IOtherStorageAppService
{
    private readonly IOtherStorageRepository _otherStorageRepository;
    private readonly IOtherStorageDetailRepository _otherStorageDetailRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;

    public OtherStorageAppService(
        IOtherStorageRepository repository, 
        IOtherStorageDetailRepository otherStorageDetailRepository,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository,
        IUniqueCodeUtils uniqueCodeUtils
        ) 
    {
        _otherStorageRepository = repository;
        _otherStorageDetailRepository = otherStorageDetailRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
    }


    [Authorize(ERPPermissions.OtherStorage.Create)]
    public async Task CreateAsync(OtherStorageCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.OtherStoragePrefix); ;
        OtherStorage otherStorage = new OtherStorage(id);
        otherStorage.Number = number;
        otherStorage.Description = input.Description;
        List<OtherStorageDetail> details = new List<OtherStorageDetail>();
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            OtherStorageDetail detail = new OtherStorageDetail(detailId);
            detail.OtherStorageId = id;
            detail.Sort = i;
            detail.ProductId = item.ProductId;
            detail.LocationId = item.LocationId;
            detail.Batch = item.Batch;
            detail.Quantity = item.Quantity;
            detail.Price = item.Price;
            details.Add(detail);
        }

        //CheckIsProductRepeat(details);

        OtherStorage result = await _otherStorageRepository.InsertAsync(otherStorage);
        await _otherStorageDetailRepository.InsertManyAsync(details);
    }



    [Authorize(ERPPermissions.OtherStorage.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        OtherStorage otherStorage = await _otherStorageRepository.FindAsync(id);
        if (otherStorage == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        if (otherStorage.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库,无法删除");
        }


        await _otherStorageRepository.DeleteAsync(otherStorage);
    }



    [Authorize(ERPPermissions.OtherStorage.Update)]
    public async Task<OtherStorageDto> GetAsync(Guid id)
    {
        var entity = await _otherStorageRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<OtherStorage, OtherStorageDto>(entity);
        return dto;
    }



    [Authorize(ERPPermissions.OtherStorage.Default)]
    public async Task<PagedResultDto<OtherStorageDto>> GetPagedListAsync(OtherStoragePagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _otherStorageRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<OtherStorageDto>(totalCount, ObjectMapper.Map<List<OtherStorage>, List<OtherStorageDto>>(result));
    }


    [Authorize(ERPPermissions.OtherStorage.Storage)]
    public async Task StoragedAsync(Guid id)
    {
        var entity = await _otherStorageRepository.GetAsync(id, true);

     

        if (entity == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (entity.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库了，不要重复入库");
        }

        //行项目部能为空
        if (entity.Details == null || entity.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }


        foreach (var item in entity.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }

        entity.IsSuccessful = true; ;
        entity.SuccessfulTime = Clock.Now;
        entity.KeeperUserId = CurrentUser.Id;

        foreach (var item in entity.Details)
        {
            var inventory = await _inventoryRepository.StorageAsync(
                locationId: item.LocationId,
                productId: item.ProductId,
                quantity: item.Quantity,
                batch: item.Batch,
                price: item.Price
                );

            InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
            inventoryLog.Number = entity.Number;
            inventoryLog.ProductId = item.ProductId;
            inventoryLog.LocationId= item.LocationId;
            inventoryLog.LogTime = Clock.Now;
            inventoryLog.LogType = InventoryLogType.OtherStorage;
            inventoryLog.Batch = item.Batch;
            inventoryLog.InQuantity = item.Quantity;
            inventoryLog.OutQuantity = 0;
            inventoryLog.AfterQuantity = inventory.Quantity;
            await _inventoryLogRepository.InsertAsync(inventoryLog);
            await _otherStorageRepository.UpdateAsync(entity);
        }
    }



    [Authorize(ERPPermissions.OtherStorage.Update)]
    public async Task UpdateAsync(Guid id, OtherStorageUpdateDto input)
    {
        OtherStorage otherStorage = await _otherStorageRepository.FindAsync(id);
        if (otherStorage == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (otherStorage.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库！无法编辑");
        }


       

        otherStorage.Description = input.Description;

        List<OtherStorageDetail> createList = new List<OtherStorageDetail>();
        List<OtherStorageDetail> updateList = new List<OtherStorageDetail>();
        List<OtherStorageDetail> deleteList = new List<OtherStorageDetail>();
        List<OtherStorageDetail> dbList = await _otherStorageDetailRepository.GetListAsync(m => m.OtherStorageId == id);

        foreach (var item in input.Details)
        {
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                OtherStorageDetail detail = new OtherStorageDetail(detailId);
                detail.OtherStorageId = id;
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                detail.Sort = item.Sort;
                createList.Add(detail);
            }
            else //编辑
            {
                OtherStorageDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                detail.Sort = item.Sort;
                updateList.Add(detail);
            }
        }
        //删除
        foreach (var dbItem in dbList)
        {
            bool isExsiting = input.Details.Any(m => m.Id == dbItem.Id);
            if (isExsiting == false)
            {
                deleteList.Add(dbItem);
            }
        }
        var result = await _otherStorageRepository.UpdateAsync(otherStorage);
        await _otherStorageDetailRepository.InsertManyAsync(createList);
        await _otherStorageDetailRepository.UpdateManyAsync(updateList);
        await _otherStorageDetailRepository.DeleteManyAsync(deleteList);
    }
}
