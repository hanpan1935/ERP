using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;
using Lanpuda.ERP.Utils.UniqueCode;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Volo.Abp.Data;
using System.Security.Cryptography;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies;

[Authorize]
public class PurchaseApplyAppService : ERPAppService, IPurchaseApplyAppService
{
    private readonly IPurchaseApplyRepository _repository;
    private readonly IPurchaseApplyDetailRepository _detailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IPurchasePriceRepository _purchasePriceRepository;
    private readonly IPurchasePriceDetailRepository _purchasePriceDetailRepository;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;


    public PurchaseApplyAppService(
        IPurchaseApplyRepository repository,
        IPurchaseApplyDetailRepository detailRepository,
        IPurchasePriceRepository purchasePriceRepository,
        IPurchasePriceDetailRepository purchasePriceDetailRepository,
        IPurchaseOrderRepository purchaseOrderRepository,
        IUniqueCodeUtils uniqueCodeUtils)
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _purchasePriceRepository = purchasePriceRepository;
        _purchasePriceDetailRepository = purchasePriceDetailRepository;
        _purchaseOrderRepository = purchaseOrderRepository;
    }

    [Authorize(ERPPermissions.PurchaseApply.Create)]
    public async Task CreateAsync(PurchaseApplyCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchaseApplyPrefix); ;
        PurchaseApply purchaseApply = new PurchaseApply(id);
        purchaseApply.Number = number;
        purchaseApply.Remark = input.Remark;
        purchaseApply.ApplyType = PurchaseApplyType.Manual;
        List<PurchaseApplyDetail> details = new List<PurchaseApplyDetail>();
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            PurchaseApplyDetail detail = new PurchaseApplyDetail(detailId);
            detail.PurchaseApplyId = id;
            detail.ProductId = item.ProductId;
            detail.Quantity = item.Quantity;
            detail.ArrivalDate = item.ArrivalDate;
            detail.Sort = i;
            details.Add(detail);
        }
        CheckIsProductRepeat(details);

        PurchaseApply result = await _repository.InsertAsync(purchaseApply);
        await _detailRepository.InsertManyAsync(details);
    }


    [Authorize(ERPPermissions.PurchaseApply.Default)]
    public async Task DeleteAsync(Guid id)
    {
        PurchaseApply purchaseApply = await _repository.FindAsync(id);
        if (purchaseApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _repository.DeleteAsync(purchaseApply);
    }


    [Authorize(ERPPermissions.PurchaseApply.Update)]
    public async Task<PurchaseApplyDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        return ObjectMapper.Map<PurchaseApply, PurchaseApplyDto>(result);
    }



    [Authorize(ERPPermissions.PurchaseApply.Default)]
    public async Task<PagedResultDto<PurchaseApplyDto>> GetPagedListAsync(PurchaseApplyGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.Number != null, m => m.Number == input.Number)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<PurchaseApplyDto>(totalCount, ObjectMapper.Map<List<PurchaseApply>, List<PurchaseApplyDto>>(result));
    }


    [Authorize(ERPPermissions.PurchaseApply.Update)]
    public async Task UpdateAsync(Guid id, PurchaseApplyUpdateDto input)
    {
        PurchaseApply purchaseApply = await _repository.FindAsync(id);
        if (purchaseApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        purchaseApply.Remark = input.Remark;

        List<PurchaseApplyDetail> createList = new List<PurchaseApplyDetail>();
        List<PurchaseApplyDetail> updateList = new List<PurchaseApplyDetail>();
        List<PurchaseApplyDetail> deleteList = new List<PurchaseApplyDetail>();
        List<PurchaseApplyDetail> dbList = await _detailRepository.GetListAsync(m => m.PurchaseApplyId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                PurchaseApplyDetail detail = new PurchaseApplyDetail(detailId);
                detail.PurchaseApplyId = id;
                detail.ProductId = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.ArrivalDate = item.ArrivalDate;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                PurchaseApplyDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.ArrivalDate = item.ArrivalDate;
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
        List<PurchaseApplyDetail> details = new List<PurchaseApplyDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);

        var result = await _repository.UpdateAsync(purchaseApply);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }


    [Authorize(ERPPermissions.PurchaseApply.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        var purchaseApply = await _repository.FindAsync(id, true);

        if (purchaseApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (purchaseApply.IsConfirmed != false)
        {
            throw new UserFriendlyException("已经确认过了");
        }

        if (purchaseApply.Details == null || purchaseApply.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        foreach (var item in purchaseApply.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("产品：" + item.Product.Name + ",数量必须大于0");
            }
        }

        purchaseApply.IsConfirmed = true;
        purchaseApply.ConfirmedTime = Clock.Now;
        purchaseApply.ConfirmeUserId = CurrentUser.Id;
        await _repository.UpdateAsync(purchaseApply);
    }



    [Authorize(ERPPermissions.PurchaseApply.Accept)]
    public async Task AcceptAsync(Guid id)
    {
        var purchaseApply = await _repository.FindAsync(id, true);

        if (purchaseApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (purchaseApply.IsAccept != false)
        {
            throw new UserFriendlyException("已经接收过了");
        }

        if (purchaseApply.Details == null || purchaseApply.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        foreach (var item in purchaseApply.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("产品：" + item.Product.Name + ",数量必须大于0");
            }
        }

        purchaseApply.IsAccept = true;
        purchaseApply.AcceptTime = Clock.Now;
        purchaseApply.AcceptUserId = CurrentUser.Id;

        //创建采购订单
        //1: 根据产品查最近便宜的报价单,不考虑是否是最新的
        //2: 根据采购报价生成采购订单
        //3: 如果供应商重复则合并


        List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();

        for (int i = 0; i < purchaseApply.Details.Count; i++)
        {
            var detail = purchaseApply.Details[i];

            //找到最低的报价
            var query = await _purchasePriceDetailRepository.WithDetailsAsync();
            query = query.Where(m => m.ProductId == detail.ProductId);
            query = query.OrderBy(m => m.Price);
            var priceDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            if (priceDetail == null)
            {
                throw new UserFriendlyException("产品:"  + detail.Product.Name + "没有采购报价!");
            }

            //创建采购订单
            var exsitsPurchaseOrder = purchaseOrderList.Where(m=>m.SupplierId == priceDetail.PurchasePrice.SupplierId).FirstOrDefault();
            if (exsitsPurchaseOrder == null)
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder(GuidGenerator.Create());
                string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchaseOrderPrefix);
                purchaseOrder.Number = number;
                purchaseOrder.SupplierId = priceDetail.PurchasePrice.SupplierId;
                if (detail.ArrivalDate != null)
                {
                    purchaseOrder.RequiredDate = (DateTime)detail.ArrivalDate;
                }
                else
                {
                    purchaseOrder.RequiredDate = Clock.Now.AddDays(priceDetail.PurchasePrice.AvgDeliveryDate);
                }
                purchaseOrder.OrderType = PurchaseOrderType.PurchaseRequest;
                //添加明细
                PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail(GuidGenerator.Create());
                purchaseOrderDetail.PurchaseOrderId = purchaseOrder.Id;
                purchaseOrderDetail.ProductId = detail.ProductId;
                purchaseOrderDetail.Quantity = detail.Quantity;
                if (detail.ArrivalDate != null)
                {
                    purchaseOrderDetail.PromiseDate = (DateTime)detail.ArrivalDate;
                }
                else
                {
                    purchaseOrderDetail.PromiseDate = Clock.Now.AddDays(priceDetail.PurchasePrice.AvgDeliveryDate);
                }
                purchaseOrderDetail.Price = priceDetail.Price;
                purchaseOrderDetail.TaxRate = priceDetail.TaxRate;
                purchaseOrderDetail.Sort = i;
                purchaseOrder.Details.Add(purchaseOrderDetail);
                purchaseOrderList.Add(purchaseOrder);
            }
            else
            {
                if (detail.ArrivalDate != null)
                {
                    var arrivalDate = (DateTime)detail.ArrivalDate;
                    if (exsitsPurchaseOrder.PromisedDate > arrivalDate)
                    {
                        exsitsPurchaseOrder.PromisedDate = arrivalDate;
                    }
                }
                PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail(GuidGenerator.Create());
                purchaseOrderDetail.PurchaseOrderId = exsitsPurchaseOrder.Id;
                purchaseOrderDetail.ProductId = detail.ProductId;
                if (detail.ArrivalDate != null)
                {
                    purchaseOrderDetail.PromiseDate = (DateTime)detail.ArrivalDate;
                }
                else
                {
                    purchaseOrderDetail.PromiseDate = Clock.Now.AddDays(priceDetail.PurchasePrice.AvgDeliveryDate);
                }
                purchaseOrderDetail.Quantity = detail.Quantity;
                purchaseOrderDetail.Price = priceDetail.Price;
                purchaseOrderDetail.TaxRate = priceDetail.TaxRate;
                purchaseOrderDetail.Sort = i;
                exsitsPurchaseOrder.Details.Add(purchaseOrderDetail);
            }


           
        }

        await _purchaseOrderRepository.InsertManyAsync(purchaseOrderList);
        await _repository.UpdateAsync(purchaseApply);
    }



    [Authorize(ERPPermissions.PurchaseApply.CreatePurchaseOrder)]
    public async Task CreatePurchaseOrder(Guid id)
    {
        var purchaseApply = await _repository.FindAsync(id, true);

        List<PurchaseOrder> PurchaseOrderList = new List<PurchaseOrder>();

        for (int i = 0; i < purchaseApply.Details.Count; i++)
        {
            var item = purchaseApply.Details[i];
            //找到价格最低的报价
            var queryable = await _purchasePriceDetailRepository.WithDetailsAsync();
            queryable = queryable.Where(m => m.ProductId == item.ProductId).OrderBy(m => m.Price);
            var priceDetail = await AsyncExecuter.FirstOrDefaultAsync(queryable);

            var order = PurchaseOrderList.Where(m => m.SupplierId == priceDetail.PurchasePrice.SupplierId).FirstOrDefault();
            if (order != null)
            {
                PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail(GuidGenerator.Create());
                purchaseOrderDetail.ProductId = item.ProductId;
                if (item.ArrivalDate != null)
                {
                    purchaseOrderDetail.PromiseDate = (DateTime)item.ArrivalDate;
                }
                else
                {
                    purchaseOrderDetail.PromiseDate = Clock.Now.AddDays(priceDetail.PurchasePrice.AvgDeliveryDate);
                }

                purchaseOrderDetail.Quantity = item.Quantity;
                purchaseOrderDetail.Price = priceDetail.Price;
                purchaseOrderDetail.TaxRate = priceDetail.TaxRate;
                purchaseOrderDetail.Sort = i;
                order.Details.Add(purchaseOrderDetail);
            }
            else
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder(GuidGenerator.Create());
                string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchaseOrderPrefix);
                purchaseOrder.Number = number;
                purchaseOrder.SupplierId = priceDetail.PurchasePrice.SupplierId;
                if (item.ArrivalDate != null)
                {
                    purchaseOrder.RequiredDate = (DateTime)item.ArrivalDate;
                }
                else
                {
                    purchaseOrder.RequiredDate = Clock.Now.AddDays(priceDetail.PurchasePrice.AvgDeliveryDate);
                }
                purchaseOrder.OrderType = PurchaseOrderType.PurchaseRequest;

                //添加订单明细
                PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail(GuidGenerator.Create());
                purchaseOrderDetail.PurchaseOrderId = purchaseOrder.Id;
                purchaseOrderDetail.ProductId = item.ProductId;
                if (item.ArrivalDate != null)
                {
                    purchaseOrderDetail.PromiseDate = (DateTime)item.ArrivalDate;
                }
                else
                {
                    purchaseOrderDetail.PromiseDate = Clock.Now.AddDays(priceDetail.PurchasePrice.AvgDeliveryDate);
                }
                purchaseOrderDetail.Quantity = item.Quantity;
                purchaseOrderDetail.TaxRate = priceDetail.TaxRate;
                purchaseOrderDetail.Sort = i;
                purchaseOrder.Details.Add(purchaseOrderDetail);
            }
        }

        await _purchaseOrderRepository.InsertManyAsync(PurchaseOrderList);
    }


    private void CheckIsProductRepeat(List<PurchaseApplyDetail> details)
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
}
