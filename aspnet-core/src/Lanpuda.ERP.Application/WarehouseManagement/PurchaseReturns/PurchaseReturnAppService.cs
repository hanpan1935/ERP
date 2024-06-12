using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Lanpuda.ERP.BasicData.Products;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns;

[Authorize]
public class PurchaseReturnAppService : ERPAppService, IPurchaseReturnAppService
{

    private readonly IPurchaseReturnRepository _purchaseReturnRepository;
    private readonly IPurchaseReturnDetailRepository _purchaseReturnDetailRepository;
    private readonly IArrivalNoticeRepository _arrivalNoticeRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;

    public PurchaseReturnAppService(
        IPurchaseReturnRepository repository,
        IPurchaseReturnDetailRepository detailRepository,
        IArrivalNoticeRepository arrivalNoticeRepository,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository,
        IPurchaseOrderRepository purchaseOrderRepository,
        IPurchaseOrderDetailRepository purchaseOrderDetailRepository
        )
    {
        _purchaseReturnRepository = repository;
        _purchaseReturnDetailRepository = detailRepository;
        _arrivalNoticeRepository = arrivalNoticeRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
        _purchaseOrderRepository = purchaseOrderRepository;
        _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
    }


    [Authorize(ERPPermissions.PurchaseReturn.Update)]
    public async Task<PurchaseReturnDto> GetAsync(Guid id)
    {
        var entity = await _purchaseReturnRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<PurchaseReturn, PurchaseReturnDto>(entity);
        return dto;
    }


    [Authorize(ERPPermissions.PurchaseReturn.Default)]
    public async Task<PagedResultDto<PurchaseReturnDto>> GetPagedListAsync(PurchaseReturnPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _purchaseReturnRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.SupplierName), m => m.PurchaseReturnApplyDetail.PurchaseReturnApply.Supplier.FullName.Contains(input.SupplierName))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name.Contains(input.ProductName))
            .WhereIf(!string.IsNullOrEmpty(input.ApplyNumber), m => m.PurchaseReturnApplyDetail.PurchaseReturnApply.Number.Contains(input.ApplyNumber))
            .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<PurchaseReturnDto>(totalCount, ObjectMapper.Map<List<PurchaseReturn>, List<PurchaseReturnDto>>(result));
    }



    [Authorize(ERPPermissions.PurchaseReturn.Update)]
    public async Task UpdateAsync(Guid id, PurchaseReturnUpdateDto input)
    {
        PurchaseReturn purchaseReturn = await _purchaseReturnRepository.FindAsync(id);
        if (purchaseReturn == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (purchaseReturn.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库！无法编辑");
        }



        //
        purchaseReturn.Remark = input.Remark;

        List<PurchaseReturnDetail> createList = new List<PurchaseReturnDetail>();
        List<PurchaseReturnDetail> updateList = new List<PurchaseReturnDetail>();
        List<PurchaseReturnDetail> deleteList = new List<PurchaseReturnDetail>();
        List<PurchaseReturnDetail> dbList = await _purchaseReturnDetailRepository.GetListAsync(m => m.PurchaseReturnId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                PurchaseReturnDetail detail = new PurchaseReturnDetail(detailId);
                detail.PurchaseReturnId = id;
                //detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                PurchaseReturnDetail detail = dbList.Where(m => m.Id == item.Id).First();

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

        var result = await _purchaseReturnRepository.UpdateAsync(purchaseReturn);
        await _purchaseReturnDetailRepository.InsertManyAsync(createList);
        await _purchaseReturnDetailRepository.UpdateManyAsync(updateList);
        await _purchaseReturnDetailRepository.DeleteManyAsync(deleteList);
    }



    [Authorize(ERPPermissions.PurchaseReturn.Out)]
    public async Task OutedAsync(Guid id)
    {
        var purchaseReturn = await _purchaseReturnRepository.GetAsync(id, true);



        if (purchaseReturn == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (purchaseReturn.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库了，不要重复出库");
        }

        //行项目部能为空
        if (purchaseReturn.Details == null || purchaseReturn.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }


        foreach (var item in purchaseReturn.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }


        purchaseReturn.SuccessfulTime = Clock.Now;
        purchaseReturn.IsSuccessful = true;
        purchaseReturn.KeeperUserId = CurrentUser.Id;

        for (int i = 0; i < purchaseReturn.Details.Count; i++)
        {
            var item = purchaseReturn.Details[i];
            double quantity = await _inventoryRepository.OutAsync(
                item.LocationId, 
                item.PurchaseReturn.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.ProductId,
                item.Quantity,
                item.PurchaseReturn.PurchaseReturnApplyDetail.PurchaseStorageDetail.Batch);

            #region 添加库存流水
            InventoryLog log = new InventoryLog(GuidGenerator.Create());
            log.Number = purchaseReturn.Number;
            log.ProductId = item.PurchaseReturn.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.ProductId;
            log.LocationId = item.LocationId;
            log.LogTime = Clock.Now;
            log.LogType = InventoryLogType.PurchaseReturn;
            log.Batch = item.Batch;
            log.OutQuantity = item.Quantity;
            log.InQuantity = 0;
            log.AfterQuantity = quantity;
            await _inventoryLogRepository.InsertAsync(log);
            #endregion
        }

        var entity = await _purchaseReturnRepository.UpdateAsync(purchaseReturn);
        var dto = ObjectMapper.Map<PurchaseReturn, PurchaseReturnDto>(entity);
    }


    [Authorize(ERPPermissions.PurchaseReturn.Update)]
    public async Task<List<PurchaseReturnDetailDto>> AutoOutAsync(Guid id)
    {
        var entity = await _purchaseReturnRepository.GetAsync(id, true);
        string batch = entity.PurchaseReturnApplyDetail.PurchaseStorageDetail.Batch;  //退货批次

        var query = await _inventoryRepository.WithDetailsAsync();
        query = query.Where(m => m.ProductId
        == entity.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.ProductId);
        query = query.Where(m=>m.Batch ==  batch);

        List<Inventory> inventories = await AsyncExecuter.ToListAsync(query);

        List<PurchaseReturnDetailDto> purchaseReturnDetailDtos = new List<PurchaseReturnDetailDto>();
        foreach (var item in inventories)
        {
            PurchaseReturnDetailDto purchaseReturnDetailDto = new PurchaseReturnDetailDto();
            purchaseReturnDetailDto.PurchaseReturnId = id;
            purchaseReturnDetailDto.ProductName = item.Product.Name;
            purchaseReturnDetailDto.ProductNumber = item.Product.Number;
            purchaseReturnDetailDto.ProductSpec = item.Product.Spec;
            purchaseReturnDetailDto.ProductUnitName = item.Product.ProductUnit.Name;
            purchaseReturnDetailDto.WarehouseId = item.Location.WarehouseId;
            purchaseReturnDetailDto.WarehouseName = item.Location.Warehouse.Name;
            purchaseReturnDetailDto.LocationId = item.LocationId;
            purchaseReturnDetailDto.LocationName = item.Location.Name;
            purchaseReturnDetailDto.Batch = item.Batch;
            purchaseReturnDetailDto.Quantity = item.Quantity;
            purchaseReturnDetailDto.CreationTime = item.CreationTime;
            purchaseReturnDetailDtos.Add(purchaseReturnDetailDto);
        }
        return purchaseReturnDetailDtos;
    }
}
