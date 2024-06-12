using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EventBus.Local;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

[Authorize]
public class SalesOutAppService : ERPAppService, ISalesOutAppService
{
    private readonly ISalesOutRepository _repository;
    private readonly ISalesOutDetailRepository _detailRepository;
    private readonly ISalesOutDetailRepository _salesOutDetailRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public SalesOutAppService(
        ISalesOutRepository repository,
        ISalesOutDetailRepository detailRepository,
        ISalesOutDetailRepository salesOutDetailRepository,
        IInventoryRepository inventoryRepository
        )
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _salesOutDetailRepository = salesOutDetailRepository;
        _inventoryRepository = inventoryRepository;
    }

    [Authorize(ERPPermissions.SalesOut.Update)]
    public async Task<SalesOutDto> GetAsync(Guid id)
    {
        SalesOut salesOut = await _repository.FindAsync(id, true);
        if (salesOut == null)
        {
            throw new EntityNotFoundException();
        }
        return ObjectMapper.Map<SalesOut, SalesOutDto>(salesOut);
    }


    [Authorize(ERPPermissions.SalesOut.Default)]
    public async Task<PagedResultDto<SalesOutDto>> GetPagedListAsync(SalesOutPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.ShipmentApplyNumber), m => m.ShipmentApplyDetail.ShipmentApply.Number.Contains(input.ShipmentApplyNumber))
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.CustomerName), m => m.ShipmentApplyDetail.ShipmentApply.Customer.FullName.Contains(input.Number))
            .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
            .WhereIf(input.KeeperUserId != null, m => m.KeeperUserId.Equals(input.KeeperUserId))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SalesOutDto>(totalCount, ObjectMapper.Map<List<SalesOut>, List<SalesOutDto>>(result));
    }

    /// <summary>
    /// 出库
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// 
    [Authorize(ERPPermissions.SalesOut.Out)]
    public async Task OutedAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);

        if (result == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (result.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库了，不要重复出库");
        }

        //行项目不能为空
        if (result.Details == null || result.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }


        foreach (var item in result.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }



        //校验数据
      
        //1 出库数量不能为负数
        foreach (var item in result.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("第" + (item.Sort + 1).ToString() + "行 , 数量错误 ，必须大于0");
            }
        }

        result.IsSuccessful = true;
        result.KeeperUserId = CurrentUser.Id;
        result.SuccessfulTime= Clock.Now;

        foreach (var item in result.Details)
        {
            await _inventoryRepository.OutAsync(
                item.LocationId, 
                item.SalesOut.ShipmentApplyDetail.SalesOrderDetail.ProductId, 
                item.Quantity, 
                item.Batch);
        }
    }


    [Authorize(ERPPermissions.SalesOut.Update)]
    public async Task UpdateAsync(Guid id, SalesOutUpdateDto input)
    {
        SalesOut salesOut = await _repository.FindAsync(id);
        if (salesOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }


        if (salesOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库！无法编辑");
        }

        salesOut.Remark = input.Remark;

        List<SalesOutDetail> createList = new List<SalesOutDetail>();
        List<SalesOutDetail> updateList = new List<SalesOutDetail>();
        List<SalesOutDetail> deleteList = new List<SalesOutDetail>();
        List<SalesOutDetail> dbList = await _detailRepository.GetListAsync(m => m.SalesOutId == id);


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                SalesOutDetail detail = new SalesOutDetail(detailId);
                detail.SalesOutId = id;
                //detail.ProductId = item.ProductId;
                //detail.ProductId = item.ou;
                detail.LocationId = item.LocationId;
                detail.Quantity = item.Quantity;
                detail.Batch = item.Batch;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                SalesOutDetail detail = dbList.Where(m => m.Id == item.Id).First();
                //detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Quantity = item.Quantity;
                detail.Batch = item.Batch;
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
        List<SalesOutDetail> details = new List<SalesOutDetail>();
        details.Union(createList);
        details.Union(updateList);


        var result = await _repository.UpdateAsync(salesOut);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }

    [Authorize(ERPPermissions.SalesReturnApply.Create)]
    [Authorize(ERPPermissions.SalesReturnApply.Update)]
    public async Task<PagedResultDto<SalesOutDetailSelectDto>> GetDetailPagedListAsync(SalesOutDetailPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _detailRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.SalesOutNumber), m => m.SalesOut.Number.Contains(input.SalesOutNumber))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Name.Contains(input.ProductName))
            .WhereIf(!string.IsNullOrEmpty(input.Batch), m => m.Batch.Contains(input.Batch))
            .WhereIf(input.CustomerId != null, m => m.SalesOut.ShipmentApplyDetail.ShipmentApply.CustomerId.Equals(input.CustomerId))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SalesOutDetailSelectDto>(totalCount, ObjectMapper.Map<List<SalesOutDetail>, List<SalesOutDetailSelectDto>>(result));
    }


    /// <summary>
    /// 自动出库
    /// </summary>
    /// <returns></returns>
    [Authorize(ERPPermissions.SalesOut.Update)]
    public async Task<List<SalesOutDetailDto>> AutoOutAsync(Guid id)
    {
        SalesOut salesOut = await _repository.FindAsync(id);
        double totalNeedQuantity = salesOut.ShipmentApplyDetail.Quantity;
        var query = await _inventoryRepository.WithDetailsAsync();
        query = query.Where(m=>m.ProductId == salesOut.ShipmentApplyDetail.SalesOrderDetail.ProductId).OrderBy(m=>m.CreationTime);
        var inventoryList = await AsyncExecuter.ToListAsync(query);

        List<SalesOutDetailDto> detailDtoList = new List<SalesOutDetailDto>();
        foreach (var inventory in inventoryList)
        {
            var sumQuantity = detailDtoList.Sum(m => m.Quantity);
            //创建明细
            SalesOutDetailDto salesOutDetail = new SalesOutDetailDto();
            salesOutDetail.SalesOutId = id;
            salesOutDetail.ProductId = inventory.ProductId;
            salesOutDetail.LocationId = inventory.LocationId;
            salesOutDetail.Batch = inventory.Batch;
            salesOutDetail.ProductName = inventory.Product.Name;
            salesOutDetail.WarehouseName = inventory.Location.Warehouse.Name;
            salesOutDetail.LocationName = inventory.Location.Name;
            //如果库存够减掉库存退出循环
            var currNeedQuantity = totalNeedQuantity - sumQuantity;
            if (inventory.Quantity >= currNeedQuantity)
            {
                salesOutDetail.Quantity = currNeedQuantity;
                detailDtoList.Add(salesOutDetail);
                break;
            }
            else //如果库存不够减掉库存继续循环
            {
                salesOutDetail.Quantity = inventory.Quantity;
                detailDtoList.Add(salesOutDetail);
            }
        }
        return detailDtoList;
    }
}
