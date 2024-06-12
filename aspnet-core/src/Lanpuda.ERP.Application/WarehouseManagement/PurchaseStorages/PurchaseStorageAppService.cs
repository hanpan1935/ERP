using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Volo.Abp;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages;

[Authorize]
public class PurchaseStorageAppService : ERPAppService, IPurchaseStorageAppService
{
    private readonly IPurchaseStorageRepository _purchaseStorageRepository;
    private readonly IPurchaseStorageDetailRepository _purchaseStorageDetailRepository;
    private readonly IArrivalNoticeRepository _arrivalNoticeRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;

    public PurchaseStorageAppService(
        IPurchaseStorageRepository repository,
        IPurchaseStorageDetailRepository detailRepository,
        IArrivalNoticeRepository arrivalNoticeRepository,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository,
        IPurchaseOrderRepository purchaseOrderRepository,
        IPurchaseOrderDetailRepository purchaseOrderDetailRepository
        )
    {
        _purchaseStorageRepository = repository;
        _purchaseStorageDetailRepository = detailRepository;
        _arrivalNoticeRepository = arrivalNoticeRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
        _purchaseOrderRepository = purchaseOrderRepository;
        _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
    }



    [Authorize(ERPPermissions.PurchaseStorage.Update)]
    public async Task<PurchaseStorageDto> GetAsync(Guid id)
    {
        var entity = await _purchaseStorageRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<PurchaseStorage, PurchaseStorageDto>(entity);
        return dto;
    }


    [Authorize(ERPPermissions.PurchaseStorage.Default)]
    public async Task<PagedResultDto<PurchaseStorageDto>> GetPagedListAsync(PurchaseStoragePagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _purchaseStorageRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.SupplierName), m => m.ArrivalNoticeDetail.PurchaseOrderDetail.PurchaseOrder.Supplier.FullName.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ArrivalNoticeNumber), m => m.ArrivalNoticeDetail.ArrivalNotice.Number.Contains(input.ArrivalNoticeNumber))
            .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<PurchaseStorageDto>(totalCount, ObjectMapper.Map<List<PurchaseStorage>, List<PurchaseStorageDto>>(result));
    }


    [Authorize(ERPPermissions.PurchaseStorage.Update)]
    public async Task UpdateAsync(Guid id, PurchaseStorageUpdateDto input)
    {
        PurchaseStorage purchaseStorage = await _purchaseStorageRepository.FindAsync(id);
        if (purchaseStorage == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (purchaseStorage.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库！无法编辑");
        }


        purchaseStorage.Remark = input.Remark;

        List<PurchaseStorageDetail> createList = new List<PurchaseStorageDetail>();
        List<PurchaseStorageDetail> updateList = new List<PurchaseStorageDetail>();
        List<PurchaseStorageDetail> deleteList = new List<PurchaseStorageDetail>();
        List<PurchaseStorageDetail> dbList = await _purchaseStorageDetailRepository.GetListAsync(m => m.PurchaseStorageId == id);
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                PurchaseStorageDetail detail = new PurchaseStorageDetail(detailId);
                detail.PurchaseStorageId = id;
                //detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                PurchaseStorageDetail detail = dbList.Where(m => m.Id == item.Id).First();
                //detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
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

        var result = await _purchaseStorageRepository.UpdateAsync(purchaseStorage);
        await _purchaseStorageDetailRepository.InsertManyAsync(createList);
        await _purchaseStorageDetailRepository.UpdateManyAsync(updateList);
        await _purchaseStorageDetailRepository.DeleteManyAsync(deleteList);
    }


    [Authorize(ERPPermissions.PurchaseStorage.Storage)]
    public async Task StoragedAsync(Guid id)
    {
        var purchaseStorage = await _purchaseStorageRepository.GetAsync(id, true);

        if (purchaseStorage == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (purchaseStorage.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库了，不要重复入库");
        }

        //行项目部能为空
        if (purchaseStorage.Details == null || purchaseStorage.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }


        foreach (var item in purchaseStorage.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }


        purchaseStorage.SuccessfulTime = Clock.Now;
        purchaseStorage.IsSuccessful = true;
        purchaseStorage.KeeperUserId = CurrentUser.Id;
        foreach (var item in purchaseStorage.Details)
        {
            var inventory = await _inventoryRepository.StorageAsync(
                item.LocationId, item.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.ProductId, 
                item.Quantity,
                item.PurchaseStorage.ArrivalNoticeDetail.ArrivalNotice.Number);
            InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
            inventoryLog.AfterQuantity = inventory.Quantity;
            inventoryLog.Number = purchaseStorage.Number;
            inventoryLog.ProductId = inventory.ProductId;
            inventoryLog.LocationId = item.LocationId;
            inventoryLog.LogTime = Clock.Now;
            inventoryLog.LogType = InventoryLogType.PurchaseStorage;
            inventoryLog.Batch = inventory.Batch;
            inventoryLog.InQuantity = item.Quantity;
            inventoryLog.OutQuantity = 0;
            await _inventoryLogRepository.InsertAsync(inventoryLog);
        }
        var entity = await _purchaseStorageRepository.UpdateAsync(purchaseStorage);
        var dto = ObjectMapper.Map<PurchaseStorage, PurchaseStorageDto>(entity);
    }


    [Authorize(ERPPermissions.PurchaseReturnApply.Update)]
    [Authorize(ERPPermissions.PurchaseReturnApply.Create)]
    public async Task<PagedResultDto<PurchaseStorageDetailSelectDto>> GetDetailPagedListAsync(PurchaseStorageDetailPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _purchaseStorageDetailRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.PurchaseStorageNumber), m => m.PurchaseStorage.Number.Contains(input.PurchaseStorageNumber))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name.Contains(input.ProductName))
            .WhereIf(!string.IsNullOrEmpty(input.Batch), m => m.Batch.Contains(input.Batch))
            .WhereIf(!string.IsNullOrEmpty(input.SupplierName), m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.PurchaseOrder.Supplier.FullName.Contains(input.SupplierName))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<PurchaseStorageDetailSelectDto>(totalCount, ObjectMapper.Map<List<PurchaseStorageDetail>, List<PurchaseStorageDetailSelectDto>>(result));
    }

}
