using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies;


[Authorize]
public class ShipmentApplyAppService : ERPAppService, IShipmentApplyAppService
{
    private readonly IShipmentApplyRepository _repository;
    private readonly IShipmentApplyDetailRepository _detailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly ISalesOutRepository _salesOutRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ISalesOutDetailRepository _salesOutDetailRepository;


    public ShipmentApplyAppService(
        IShipmentApplyRepository repository,
        IShipmentApplyDetailRepository detailRepository,
        ISalesOutRepository salesOutRepository,
        IInventoryRepository inventoryRepository,
        ISalesOutDetailRepository salesOutDetailRepository,
        IUniqueCodeUtils uniqueCodeUtils)
    {

        _repository = repository;
        _detailRepository = detailRepository;
        _salesOutRepository = salesOutRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _inventoryRepository = inventoryRepository;
        _salesOutDetailRepository = salesOutDetailRepository;
    }

    [Authorize(ERPPermissions.ShipmentApply.Create)]
    public async Task CreateAsync(ShipmentApplyCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.ShipmentApplyPrefix); 
        ShipmentApply shipmentApply = new ShipmentApply(id);
        shipmentApply.Number = number;
        shipmentApply.CustomerId = input.CustomerId;
        shipmentApply.Address = input.Address;
        shipmentApply.Consignee = input.Consignee;
        shipmentApply.ConsigneeTel = input.ConsigneeTel;

        List<ShipmentApplyDetail> details = new List<ShipmentApplyDetail>();


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            ShipmentApplyDetail detail = new ShipmentApplyDetail(detailId);
            detail.ShipmentApplyId = id;
            detail.SalesOrderDetailId = item.SalesOrderDetailId;
            detail.Quantity = item.Quantity;
            detail.Sort = i;
            details.Add(detail);
        }

        CheckIsProductRepeat(details);

        ShipmentApply result = await _repository.InsertAsync(shipmentApply);
        await _detailRepository.InsertManyAsync(details);
        var entity = await _repository.FindAsync(id, true);
    }


    [Authorize(ERPPermissions.ShipmentApply.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        ShipmentApply shipmentApply = await _repository.FindAsync(id);
        if (shipmentApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }


        foreach (var detail in shipmentApply.Details)
        {
            var salesOut = await _salesOutRepository.FirstOrDefaultAsync(m => m.ShipmentApplyDetailId == detail.Id);
            if (salesOut != null)
            {
                if (salesOut.IsSuccessful == true)
                {
                    throw new UserFriendlyException("已经出库，无法删除");
                }
                else
                {
                    await _salesOutRepository.DeleteAsync(salesOut);
                }
            }
        }
        await _repository.DeleteAsync(shipmentApply);
    }


    [Authorize(ERPPermissions.ShipmentApply.Confirm)]
    public async Task ConfirmAsync(Guid id)
    {
        ShipmentApply shipmentApply = await _repository.FindAsync(id);
        if (shipmentApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (shipmentApply.IsConfirmed == true)
        {
            return;
        }

        if (shipmentApply.Details == null || shipmentApply.Details.Count == 0)
        {
            throw new UserFriendlyException("申请明细不能为空");
        }

        foreach (var item in shipmentApply.Details)
        {
            if (item.Quantity < 0)
            {
                throw new UserFriendlyException("申请数量不能为负数");
            }
        }

        shipmentApply.IsConfirmed = true;
        shipmentApply.ConfirmeTime = Clock.Now;
        shipmentApply.ConfirmeUserId = CurrentUser.Id;

        //生成销售出库单
        foreach (var item in shipmentApply.Details)
        {
            SalesOut salesOut = new SalesOut(GuidGenerator.Create());
            string outNumber = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.SalesOutPrefix);
            salesOut.Number = outNumber;
            salesOut.ShipmentApplyDetailId = item.Id;
            item.SalesOut = salesOut;
        }
        await _repository.UpdateAsync(shipmentApply);
    }


    [Authorize(ERPPermissions.ShipmentApply.Update)]
    public async Task<ShipmentApplyDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        return ObjectMapper.Map<ShipmentApply, ShipmentApplyDto>(result);
    }

    [Authorize(ERPPermissions.ShipmentApply.Default)]
    public async Task<PagedResultDto<ShipmentApplyDto>> GetPagedListAsync(ShipmentApplyPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.CustomerName), m => m.Customer.FullName.Contains(input.CustomerName))
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed.Equals(input.IsConfirmed))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<ShipmentApplyDto>(totalCount, ObjectMapper.Map<List<ShipmentApply>, List<ShipmentApplyDto>>(result));
    }

    [Authorize(ERPPermissions.ShipmentApply.Update)]
    public async Task UpdateAsync(Guid id, ShipmentApplyUpdateDto input)
    {
        ShipmentApply shipmentApply = await _repository.FindAsync(id);
        if (shipmentApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //状态验证 只有未确认的才能编辑 IsConfirmed = false;
        if (shipmentApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("无法编辑已经确认提交的申请单");
        }
        shipmentApply.CustomerId = input.CustomerId;
        shipmentApply.Address = input.Address;
        shipmentApply.Consignee = input.Consignee;
        shipmentApply.ConsigneeTel = input.ConsigneeTel;

        List<ShipmentApplyDetail> createList = new List<ShipmentApplyDetail>();
        List<ShipmentApplyDetail> updateList = new List<ShipmentApplyDetail>();
        List<ShipmentApplyDetail> deleteList = new List<ShipmentApplyDetail>();
        List<ShipmentApplyDetail> dbList = await _detailRepository.GetListAsync(m => m.ShipmentApplyId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item    = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                ShipmentApplyDetail detail = new ShipmentApplyDetail(detailId);
                detail.ShipmentApplyId = id;
                detail.SalesOrderDetailId = item.SalesOrderDetailId;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                ShipmentApplyDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.SalesOrderDetailId = item.SalesOrderDetailId;
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


        //判重
        List<ShipmentApplyDetail> details = new List<ShipmentApplyDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);

        var result = await _repository.UpdateAsync(shipmentApply);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }


    //TODO 判断是否重复
    private void CheckIsProductRepeat(List<ShipmentApplyDetail> details)
    {
        var res = from m in details group m by m.SalesOrderDetailId;
        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += (item.Sort + 1) + "行,";
                }
                throw new UserFriendlyException("第:" + rowNumbers + "订单重复");
            }
        }
    }
}
