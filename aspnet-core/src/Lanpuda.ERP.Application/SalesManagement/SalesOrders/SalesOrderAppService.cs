using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.ProductionManagement.Mpses;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;
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

namespace Lanpuda.ERP.SalesManagement.SalesOrders;


[Authorize]
public class SalesOrderAppService : ERPAppService, ISalesOrderAppService
{
    //protected override string GetPolicyName { get; set; } = ERPPermissions.SalesOrder.Default;
    //protected override string GetListPolicyName { get; set; } = ERPPermissions.SalesOrder.Default;
    //protected override string CreatePolicyName { get; set; } = ERPPermissions.SalesOrder.Create;
    //protected override string UpdatePolicyName { get; set; } = ERPPermissions.SalesOrder.Update;
    //protected override string DeletePolicyName { get; set; } = ERPPermissions.SalesOrder.Delete;

    private readonly ISalesOrderRepository _repository;
    private readonly ISalesOrderDetailRepository _detailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IShipmentApplyDetailRepository _shipmentApplyDetailRepository;
    private readonly ISalesReturnApplyDetailRepository _salesReturnApplyDetailRepository;
    private readonly ISalesOutDetailRepository _salesOutDetailRepository;
    private readonly ISalesReturnDetailRepository _salesReturnDetailRepository;
    private readonly IMpsRepository _mpsRepository;


    public SalesOrderAppService(
        ISalesOrderRepository repository,
        ISalesOrderDetailRepository detailRepository,
        IUniqueCodeUtils uniqueCodeUtils,
        ISalesOutDetailRepository salesOutDetailRepository,
        IInventoryRepository inventoryRepository,
        IShipmentApplyDetailRepository shipmentApplyDetailRepository,
        ISalesReturnApplyDetailRepository salesReturnApplyDetailRepository,
        ISalesReturnDetailRepository salesReturnDetailRepository,
        IMpsRepository mpsRepository
        )
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _salesOutDetailRepository = salesOutDetailRepository;
        _inventoryRepository = inventoryRepository;
        _shipmentApplyDetailRepository = shipmentApplyDetailRepository;
        _salesReturnApplyDetailRepository = salesReturnApplyDetailRepository;
        _salesReturnDetailRepository = salesReturnDetailRepository;
        _mpsRepository = mpsRepository;

    }

    [Authorize(ERPPermissions.SalesOrder.Create)]
    public async Task CreateAsync(SalesOrderCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.SalesOrderPrefix); ;
        SalesOrder salesOrder = new SalesOrder(id);
        salesOrder.Number = number;
        salesOrder.CustomerId = input.CustomerId;
        salesOrder.RequiredDate = input.RequiredDate;
        salesOrder.PromisedDate = input.PromisedDate;
        salesOrder.OrderType = input.OrderType;
        salesOrder.Description = input.Description;
        salesOrder.IsConfirmed = false;
        salesOrder.CloseStatus = SalesOrderCloseStatus.ToBeClosed;

        List<SalesOrderDetail> details = new List<SalesOrderDetail>();

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            SalesOrderDetail detail = new SalesOrderDetail(detailId);
            detail.SalesOrderId = id;
            detail.Sort = i;
            detail.DeliveryDate = item.DeliveryDate;
            detail.Quantity = item.Quantity;
            detail.ProductId = item.ProductId;
            detail.Price = item.Price;
            detail.TaxRate = item.TaxRate;
            detail.Requirement = item.Requirement;
            details.Add(detail);
        }

        CheckIsProductRepeat(details);
        SalesOrder result = await _repository.InsertAsync(salesOrder);
        await _detailRepository.InsertManyAsync(details);
    }



    [Authorize(ERPPermissions.SalesOrder.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        SalesOrder salesOrder = await _repository.FindAsync(id);
        if (salesOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

       

        foreach (var item in salesOrder.Details)
        {
            //查询发货申请
            var hasShipmentAppl = await _shipmentApplyDetailRepository.AnyAsync(m => m.SalesOrderDetailId == item.Id);
            if (hasShipmentAppl)
            {
                throw new UserFriendlyException("请先删除对应的发货申请");
            }
            //查询MPS
            var hasMps = await _mpsRepository.AnyAsync(m=>m.SalesOrderDetailId ==item.Id);
            if (hasMps)
            {
                throw new UserFriendlyException("请先删除对应的生产计划");
            }
        }

        await _repository.DeleteAsync(salesOrder);
    }


    [Authorize(ERPPermissions.SalesOrder.Confirm)]
    public async Task ConfirmAsync(Guid id)
    {
        SalesOrder salesOrder = await _repository.FindAsync(id);
        if (salesOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (salesOrder.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认了");
        }

        if (salesOrder.Details == null || salesOrder.Details.Count == 0)
        {
            throw new UserFriendlyException("订单明细不能为空");
        }

        foreach (var item in salesOrder.Details)
        {
            if (item.Price < 0)
            {
                throw new UserFriendlyException("产品：" + item.Product.Name + ",价格不能为负数");
            }

            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("产品：" + item.Product.Name + ",数量必须大于0");
            }
        }

        salesOrder.IsConfirmed = true;
        salesOrder.ConfirmeTime = Clock.Now;
        salesOrder.ConfirmeUserId = CurrentUser.Id;
        await _repository.UpdateAsync(salesOrder);
    }



    [Authorize(ERPPermissions.SalesOrder.Update)]
    public async Task<SalesOrderGetDto> GetAsync(Guid id)
    {
        SalesOrder result = await _repository.FindAsync(id, true);
        if (result == null)
        {
            throw new EntityNotFoundException("没有查询到Id为:" + id + "的销售订单!");
        }
        var orderDto = ObjectMapper.Map<SalesOrder, SalesOrderGetDto>(result);
        return orderDto;
    }


    [Authorize(ERPPermissions.SalesOrder.Close)]
    public async Task CloseOrderAsync(Guid id)
    {
        SalesOrder result = await _repository.FindAsync(id, true);
        if (result == null)
        {
            throw new EntityNotFoundException("没有查询到Id为:" + id + "的销售订单!");
        }
        if (result.CloseStatus != SalesOrderCloseStatus.ToBeClosed)
        {
            throw new UserFriendlyException("已经关闭了");
        }

        result.CloseStatus = SalesOrderCloseStatus.ManualClosed;
        await _repository.UpdateAsync(result);
        var orderDto = ObjectMapper.Map<SalesOrder, SalesOrderDto>(result);
    }



    [Authorize(ERPPermissions.SalesOrder.Default)]
    public async Task<PagedResultDto<SalesOrderDto>> GetPagedListAsync(SalesOrderPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.CustomerId != null, m => m.CustomerId == input.CustomerId)
            .WhereIf(!string.IsNullOrEmpty(input.CustomerName), m => m.Customer.FullName.Contains(input.CustomerName))
            .WhereIf(input.RequiredDateStart != null, m => m.RequiredDate >= input.RequiredDateStart)
            .WhereIf(input.RequiredDateEnd != null, m => m.RequiredDate <= input.RequiredDateEnd)
            .WhereIf(input.PromisedDateStart != null, m => m.PromisedDate >= input.PromisedDateStart)
            .WhereIf(input.PromisedDateEnd != null, m => m.PromisedDate <= input.PromisedDateEnd)
            .WhereIf(input.OrderType != null, m => m.OrderType == input.OrderType)
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed == input.IsConfirmed)
            .WhereIf(input.CloseStatus != null, m => m.CloseStatus == input.CloseStatus)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<SalesOrderDto>(totalCount, ObjectMapper.Map<List<SalesOrder>, List<SalesOrderDto>>(result));
    }


    [Authorize(ERPPermissions.SalesOrder.Update)]
    public async Task UpdateAsync(Guid id, SalesOrderUpdateDto input)
    {
        SalesOrder salesOrder = await _repository.FindAsync(id);
        if (salesOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //状态验证 只有未确认的才能编辑 IsConfirmed = false;
        if (salesOrder.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认无法编辑");
        }

        salesOrder.CustomerId = input.CustomerId;
        salesOrder.RequiredDate = input.RequiredDate;
        salesOrder.PromisedDate = input.PromisedDate;
        salesOrder.OrderType = input.OrderType;
        salesOrder.Description = input.Description;
        salesOrder.IsConfirmed = false;

        List<SalesOrderDetail> createList = new List<SalesOrderDetail>();
        List<SalesOrderDetail> updateList = new List<SalesOrderDetail>();
        List<SalesOrderDetail> deleteList = new List<SalesOrderDetail>();
        List<SalesOrderDetail> dbList = await _detailRepository.GetListAsync(m => m.SalesOrderId == id);


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                SalesOrderDetail detail = new SalesOrderDetail(detailId);
                detail.SalesOrderId = id;
                detail.Sort = i;
                detail.DeliveryDate = item.DeliveryDate;
                detail.Quantity = item.Quantity;
                detail.ProductId = item.ProductId;
                detail.Price = item.Price;
                detail.TaxRate = item.TaxRate;
                detail.Requirement = item.Requirement;
                createList.Add(detail);
            }
            else //编辑
            {
                SalesOrderDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.DeliveryDate = item.DeliveryDate;
                detail.Quantity = item.Quantity;
                detail.ProductId = item.ProductId;
                detail.Price = item.Price;
                detail.TaxRate = item.TaxRate;
                detail.Requirement = item.Requirement;
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
        List<SalesOrderDetail> details = new List<SalesOrderDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);


        var result = await _repository.UpdateAsync(salesOrder);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }


    //public async Task<SalesOrderProfileDto> GetProfileAsync(Guid id)
    //{
    //    SalesOrder result = await _repository.FindAsync(id, true);
    //    SalesOrderProfileDto dto = new SalesOrderProfileDto();
    //    dto.Id = result.Id;
    //    dto.Number = result.Number;
    //    dto.CustomerId = result.CustomerId;
    //    dto.CustomerFullName = result.Customer.FullName;
    //    dto.CustomerShortName = result.Customer.ShortName;
    //    dto.RequiredDate = result.RequiredDate;
    //    dto.PromisedDate = result.PromisedDate;
    //    dto.OrderType = result.OrderType;
    //    dto.Description = result.Description;
    //    dto.CloseStatus = result.CloseStatus;
    //    dto.CreatorSurname = result.Creator.Surname;
    //    dto.CreatorName = result.Creator.Name;
    //    dto.IsConfirmed = result.IsConfirmed;
    //    dto.ConfirmeTime = result.ConfirmeTime;
    //    dto.ConfirmeUserId = result.ConfirmeUserId;
    //    if (result.ConfirmeUser != null)
    //    {
    //        dto.ConfirmeUserSurname = result.ConfirmeUser.Surname;
    //        dto.ConfirmeUserName = result.ConfirmeUser.Name;
    //    }

    //    result.Details = result.Details.OrderByDescending(m => m.Id).ToList();
    //    foreach (var detail in result.Details)
    //    {
    //        SalesOrderProfileDetailDto detailDto = new SalesOrderProfileDetailDto();
    //        detailDto.SalesOrderDetailId = detail.Id;
    //        detailDto.DeliveryDate = detail.DeliveryDate;
    //        detailDto.Quantity = detail.Quantity;
    //        detailDto.ProductId = detail.ProductId;
    //        detailDto.ProductName = detail.Product.Name;
    //        detailDto.ProductNumber = detail.Product.Number;
    //        detailDto.ProductUnitName = detail.Product.ProductUnit.Name;
    //        detailDto.ProductSpec = detail.Product.Spec;
    //        detailDto.Price = detail.Price;
    //        detailDto.TaxRate = detail.TaxRate;
    //        detailDto.Requirement = detail.Requirement;
    //        detailDto.InventoryQuantity = await _inventoryRepository.GetSumQuantityByProductIdAsync(detail.ProductId);
    //        //发货申请
    //        var shipmentApplyDetailList = await _shipmentApplyDetailRepository.GetListAsync(m => m.SalesOrderDetailId == detail.Id, true);
    //        foreach (var shipmentApplyDetail in shipmentApplyDetailList)
    //        {
    //            if (shipmentApplyDetail.ShipmentApply.IsConfirmed == false)
    //            {
    //                continue;
    //            }
    //            SalesOrderProfileShipmentApplyDetailDto shipmentApplyDetailDto = new SalesOrderProfileShipmentApplyDetailDto();
    //            shipmentApplyDetailDto.ShipmentApplyDetailId = shipmentApplyDetail.Id;
    //            shipmentApplyDetailDto.ShipmentApplyNumber = shipmentApplyDetail.ShipmentApply.Number;
    //            shipmentApplyDetailDto.Quantity = shipmentApplyDetail.Quantity;
    //            detailDto.ShipmentApplyDetails.Add(shipmentApplyDetailDto);
    //        }


    //        ////销售出库
    //        //var salesOutDetailList = await _salesOutDetailRepository.GetListAsync(m => m.SalesOut.ShipmentApplyId == detail.Id);
    //        //foreach (var item in salesOutDetailList)
    //        //{
    //        //    SalesOrderProfileOutDetailDto outDetailDto = new SalesOrderProfileOutDetailDto();
    //        //    outDetailDto.SalesOutDetailId = item.Id;
    //        //    outDetailDto.SalesOutNumber = item.SalesOut.Number;
    //        //    outDetailDto.Quantity = item.Quantity;

    //        //    detailDto.OutDetails.Add(outDetailDto);
    //        //}

    //        //退货申请
    //        var returnApplyDetailList = await _salesReturnApplyDetailRepository.GetListAsync(m => m.SalesOutDetailId == detail.Id);
    //        foreach (var returnApplyDetail in returnApplyDetailList)
    //        {
    //            SalesOrderProfileReturnApplyDetailDto returnApplyDetailDto = new SalesOrderProfileReturnApplyDetailDto();
    //            returnApplyDetailDto.SalesReturnApplyDetailId = returnApplyDetail.Id; ;
    //            returnApplyDetailDto.SalesReturnApplyNumber = returnApplyDetail.SalesReturnApply.Number;
    //            returnApplyDetailDto.Quantity = returnApplyDetail.Quantity;
    //            detailDto.ReturnApplyDetails.Add(returnApplyDetailDto);
    //        }



    //        ////销售退货
    //        //var salesReturnDetailList = await _salesReturnDetailRepository.GetListAsync(m=>m.SalesOrderDetailId == detail.Id);  
    //        //foreach (var item in salesReturnDetailList)
    //        //{
    //        //    SalesOrderProfileReturnDetailDto returnDetailDto = new SalesOrderProfileReturnDetailDto();
    //        //    returnDetailDto.SalesReturnDetailId = item.Id;
    //        //    returnDetailDto.SalesRetrunNumber = item.SalesReturn.Number;
    //        //    returnDetailDto.Quantity = item.Quantity;
    //        //    detailDto.ReturnDetails.Add(returnDetailDto);
    //        //}

    //        //生产计划
    //        var mpsList = await _mpsRepository.GetListAsync(m => m.SalesOrderDetailId == detail.Id);
    //        foreach (var item in mpsList)
    //        {
    //            SalesOrderProfileMpsDto mpsDto = new SalesOrderProfileMpsDto();
    //            mpsDto.MpsId = item.Id;
    //            mpsDto.MpsNumber = item.Number;
    //            mpsDto.Quanity = item.Quantity;
    //            detailDto.Mpses.Add(mpsDto);
    //        }

    //        dto.Details.Add(detailDto);
    //    }
    //    return dto;
    //}


    [Authorize(ERPPermissions.ShipmentApply.Update)]
    [Authorize(ERPPermissions.ShipmentApply.Create)]
    public async Task<PagedResultDto<SalesOrderDetailSelectDto>> GetDetailPagedListAsync(SalesOrderDetailPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }

        var query = await _detailRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.SalesOrderNumber), m => m.SalesOrder.Number.Contains(input.SalesOrderNumber))
            .WhereIf(input.CustomerId != null, m => m.SalesOrder.CustomerId == input.CustomerId)
            .WhereIf(!string.IsNullOrEmpty(input.CustomerName), m => m.SalesOrder.Customer.BankName.Contains(input.CustomerName))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.Product.Name.Contains(input.ProductName))
            .WhereIf(input.OrderType != null, m => m.SalesOrder.OrderType >= input.OrderType)
            .WhereIf(input.CloseStatus != null, m => m.SalesOrder.CloseStatus == input.CloseStatus)
            .WhereIf(input.IsConfirmed != null, m => m.SalesOrder.IsConfirmed == input.IsConfirmed)
            .WhereIf(input.DeliveryDateStart != null, m => m.DeliveryDate >= input.DeliveryDateStart)
            .WhereIf(input.DeliveryDateEnd != null, m => m.DeliveryDate <= input.DeliveryDateEnd)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        var detailList = ObjectMapper.Map<List<SalesOrderDetail>, List<SalesOrderDetailSelectDto>>(result);

        return new PagedResultDto<SalesOrderDetailSelectDto>(totalCount, detailList);
    }


    private void CheckIsProductRepeat(List<SalesOrderDetail> details)
    {
        var res = from m in details group m by m.ProductId;

        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += (item.Sort + 1) + "";
                }
                throw new UserFriendlyException("第:" + rowNumbers + "产品重复");
            }
        }
    }

    [Authorize(ERPPermissions.SalesOrder.CreateMps)]
    public async Task CreateMpsAsync(Guid id)
    {
        var salesOrder = await _repository.FindAsync(id, true);

        if (salesOrder == null) throw new UserFriendlyException("销售订单不存在");

        if (salesOrder.IsConfirmed == false)
        {
            throw new UserFriendlyException("销售订单还未确认");
        }

        List<Mps> mpses = new List<Mps>();
        foreach (var item in salesOrder.Details)
        {
            Mps mps = new Mps(GuidGenerator.Create());
            string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.MpsPrefix);
            mps.Number = number;
            mps.MpsType = MpsType.Customer;
            mps.SalesOrderDetailId = item.Id;
            mps.StartDate = Clock.Now;
            mps.CompleteDate = item.DeliveryDate;
            mps.ProductId = item.ProductId;
            mps.Quantity = item.Quantity;
            mpses.Add(mps);
        }
        await _mpsRepository.InsertManyAsync(mpses);
    }
}
