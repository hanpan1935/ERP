using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Volo.Abp.EventBus.Local;
using System.Data;
using Volo.Abp;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns;


[Authorize]
public class SalesReturnAppService : ERPAppService, ISalesReturnAppService
{

    private readonly ISalesReturnRepository _repository;
    private readonly ISalesReturnDetailRepository _detailRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;

    public SalesReturnAppService(
        ISalesReturnRepository repository,
        ISalesReturnDetailRepository detailRepository,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository
        )
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
    }


    [Authorize(ERPPermissions.SalesReturn.Update)]
    public async Task<SalesReturnDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<SalesReturn, SalesReturnDto>(result);
    }


    [Authorize(ERPPermissions.SalesReturn.Default)]
    public async Task<PagedResultDto<SalesReturnDto>> GetPagedListAsync(SalesReturnPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Name.Contains(input.ProductName))
            .WhereIf(!string.IsNullOrEmpty(input.ApplyNumber), m => m.SalesReturnApplyDetail.SalesReturnApply.Number.Contains(input.ApplyNumber))
            .WhereIf(!string.IsNullOrEmpty(input.CustomerName), m => m.SalesReturnApplyDetail.SalesReturnApply.Customer.FullName.Contains(input.CustomerName))
            .WhereIf(input.Reason != null, m => m.SalesReturnApplyDetail.SalesReturnApply.Reason.Equals(input.Reason))
            .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SalesReturnDto>(totalCount, ObjectMapper.Map<List<SalesReturn>, List<SalesReturnDto>>(result));
    }


    /// <summary>
    /// 入库
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// 

    [Authorize(ERPPermissions.SalesReturn.Storage)]
    public async Task StoragedAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);

        if (result == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (result.Details == null || result.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        if (result.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库");
        }

        foreach (var item in result.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("入库数量不能小于或者等于0");
            }
        }


        DateTime now = Clock.Now;
        result.IsSuccessful = true;
        result.SuccessfulTime = now;
        result.KeeperUserId = CurrentUser.Id;
        foreach (var item in result.Details)
        {
            var inventory = await _inventoryRepository.StorageAsync(
                 locationId: item.LocationId,
                 productId: item.SalesReturn.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.ProductId,
                 batch: item.SalesReturn.SalesReturnApplyDetail.SalesOutDetail.Batch,
                 quantity: item.Quantity
                 );
            InventoryLog log = new InventoryLog(GuidGenerator.Create());
            log.Number = result.Number;
            log.ProductId = inventory.ProductId;
            log.LocationId = inventory.LocationId;
            log.LogTime = now;
            log.LogType = InventoryLogType.SalesReturn;
            log.Batch = inventory.Batch;
            log.InQuantity = item.Quantity;
            log.OutQuantity = 0;
            log.AfterQuantity = inventory.Quantity;
            await _inventoryLogRepository.InsertAsync(log);
        }
        await _repository.UpdateAsync(result);
    }




    [Authorize(ERPPermissions.SalesReturn.Update)]
    public async Task UpdateAsync(Guid id, SalesReturnUpdateDto input)
    {
        SalesReturn salesReturn = await _repository.FindAsync(id);
        if (salesReturn == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        if (salesReturn.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库！无法编辑");
        }


        salesReturn.Remark = input.Remark;

        List<SalesReturnDetail> createList = new List<SalesReturnDetail>();
        List<SalesReturnDetail> updateList = new List<SalesReturnDetail>();
        List<SalesReturnDetail> deleteList = new List<SalesReturnDetail>();
        List<SalesReturnDetail> dbList = await _detailRepository.GetListAsync(m => m.SalesReturnId == id);


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                SalesReturnDetail detail = new SalesReturnDetail(detailId);
                detail.SalesReturnId = id;
                //detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Quantity = item.Quantity;
                //detail.Batch = item.Batch;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                SalesReturnDetail detail = dbList.Where(m => m.Id == item.Id).First();
                //detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Quantity = item.Quantity;
                //detail.Batch = item.Batch;
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


        //判重
        List<SalesReturnDetail> details = new List<SalesReturnDetail>();
        details.Union(createList);
        details.Union(updateList);


        var result = await _repository.UpdateAsync(salesReturn);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }
}
