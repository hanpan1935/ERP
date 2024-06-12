using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.Utils.UniqueCode;
using Volo.Abp;
using Lanpuda.ERP.ProductionManagement.Boms;
using Lanpuda.ERP.QualityManagement.FinalInspections;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Volo.Abp.Domain.Repositories;
using Lanpuda.ERP.Permissions;
using Microsoft.AspNetCore.Authorization;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.ProductionManagement.Mrps;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using StackExchange.Redis;
using System.Collections;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies;
using Lanpuda.ERP.BasicData.Products;

namespace Lanpuda.ERP.ProductionManagement.Mpses;

//[Authorize]
public class MpsAppService : ERPAppService, IMpsAppService
{
    private readonly IMpsRepository _repository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IBomRepository _bomRepository;
    private readonly IBomDetailRepository _bomDetailRepository;
    private readonly IWorkOrderRepository _workOrderRepository;
    private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;
    private readonly ISalesOrderRepository _salesOrderRepository;
    private readonly IPurchaseApplyRepository _purchaseApplyRepository;
    private readonly IMrpDetailRepository _mrpDetailRepository;
    private readonly WorkOrderManager _workOrderManager;


    public MpsAppService(
        IMpsRepository repository,
        IUniqueCodeUtils uniqueCodeUtils,
        IBomRepository bomRepository,
        IBomDetailRepository bomDetailRepository,
        IWorkOrderRepository workOrderRepository,
        ISalesOrderDetailRepository salesOrderDetailRepository,
        IPurchaseApplyRepository purchaseApplyRepository,
        ISalesOrderRepository salesOrderRepository,
        IMrpDetailRepository mrpDetailRepository,
        WorkOrderManager workOrderManager
        )
    {
        _repository = repository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _bomRepository = bomRepository;
        _bomDetailRepository = bomDetailRepository;
        _workOrderRepository = workOrderRepository;
        _salesOrderDetailRepository = salesOrderDetailRepository;
        _salesOrderRepository = salesOrderRepository;
        _purchaseApplyRepository = purchaseApplyRepository;
        _mrpDetailRepository = mrpDetailRepository;
        _workOrderManager = workOrderManager;
    }


    [Authorize(ERPPermissions.Mps.Create)]
    public async Task CreateAsync(MpsCreateDto input)
    {

        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.MpsPrefix);
        Mps mps = new Mps(id);
        mps.Number = number;
        mps.MpsType = input.MpsType;
        mps.StartDate = input.StartDate;
        mps.CompleteDate = input.CompleteDate;
        mps.ProductId = input.ProductId;
        mps.Quantity = input.Quantity;
        mps.Remark = input.Remark;

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            if (item.Quantity <= 0)
            {
                continue;
            }
            MpsDetail mpsDetail = new MpsDetail(GuidGenerator.Create());
            mpsDetail.MpsId = id;
            mpsDetail.ProductionDate = item.ProductionDate;
            mpsDetail.Quantity = item.Quantity;
            mpsDetail.Remark = item.Remark;
            mps.Details.Add(mpsDetail);
        }

        Mps result = await _repository.InsertAsync(mps);
    }


    [Authorize(ERPPermissions.Mps.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Mps mps = await _repository.FindAsync(id);
        if (mps == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //查询有没有生产工单
        var any = await _workOrderRepository.AnyAsync(m => m.MpsId == id);
        if (any)
        {
            throw new UserFriendlyException("请先删除计划对应的生产工单");
        }

        if (mps.PurchaseApply != null)
        {
            throw new UserFriendlyException("请先删除计划对应的采购申请");
        }


        await _repository.DeleteAsync(mps);
    }


    [Authorize(ERPPermissions.Mps.Update)]
    public async Task<MpsDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<Mps, MpsDto>(result);
    }


    [Authorize(ERPPermissions.Mps.Default)]
    public async Task<PagedResultDto<MpsDto>> GetPagedListAsync(MpsPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.Product.Name.Contains(input.ProductName))
            .WhereIf(input.MpsType != null, m => m.MpsType.Equals(input.MpsType))
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed.Equals(input.IsConfirmed))
            .WhereIf(input.StartDateStart != null, m => m.StartDate >= (input.StartDateStart))
            .WhereIf(input.StartDateEnd != null, m => m.StartDate <= (input.StartDateEnd))
            .WhereIf(input.CompleteDateStart != null, m => m.CompleteDate >= (input.CompleteDateStart))
            .WhereIf(input.CompleteDateEnd != null, m => m.CompleteDate <= (input.CompleteDateEnd))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting);
        query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<MpsDto>(totalCount, ObjectMapper.Map<List<Mps>, List<MpsDto>>(result));
    }


    [Authorize(ERPPermissions.Mps.Update)]
    public async Task UpdateAsync(Guid id, MpsUpdateDto input)
    {

        Mps mps = await _repository.FindAsync(id, true);
        if (mps == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //状态验证 只有未确认的才能编辑 IsConfirmed = false;
        if (mps.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认无法编辑");
        }
        mps.MpsType = input.MpsType;
        mps.StartDate = input.StartDate;
        mps.CompleteDate = input.CompleteDate;
        mps.ProductId = input.ProductId;
        mps.Quantity = input.Quantity;
        mps.Remark = input.Remark;


        mps.Details.Clear();
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            if (item.Quantity <= 0)
            {
                continue;
            }

            Guid detailId = GuidGenerator.Create();
            MpsDetail detail = new MpsDetail(detailId);
            detail.MpsId = id;
            detail.ProductionDate = item.ProductionDate;
            detail.Quantity = item.Quantity;
            detail.Remark = item.Remark;
            mps.Details.Add(detail);
        }

        var result = await _repository.UpdateAsync(mps);
    }


    [Authorize(ERPPermissions.Mps.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        Mps mps = await _repository.FindAsync(id);
        if (mps == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (mps.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认了，无法再次确认");
        }


        foreach (var item in mps.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException(item.ProductionDate.ToShortDateString() + ",数量不能小于0");
            }
        }

        mps.IsConfirmed = true;
        mps.ConfirmedUserId = CurrentUser.Id;
        mps.ConfirmedTime = Clock.Now;

        //创建产品终检单

        if (mps.Product.IsFinalInspection == true)
        {
            FinalInspection finalInspection = new FinalInspection(GuidGenerator.Create());
            string number = await _uniqueCodeUtils.GetUniqueNumberAsync("FI");
            finalInspection.Number = number;
            finalInspection.MpsId = mps.Id;
            mps.FinalInspection = finalInspection;
        }

        await _repository.UpdateAsync(mps);
    }



    [Authorize(ERPPermissions.Mps.MRP)]
    public async Task CreateMrpAsync(Guid id)
    {
        var mps = await _repository.FindAsync(id);
        var bomQueryable = await _bomRepository.WithDetailsAsync();
        bomQueryable = bomQueryable.Where(b => b.ProductId == mps.ProductId);
        Bom bom = await AsyncExecuter.FirstOrDefaultAsync(bomQueryable);
        if (bom == null) { throw new UserFriendlyException("主产品Bom不存在"); }

        List<BomDetailDto> BomDetailDtoList = new List<BomDetailDto>();//用于显示物料清单
        List<MrpNode> nodeList = new List<MrpNode>();//取得所有需要mrp运算的mrpnode


        //存放主产品BOM
        BomDetailDto rootBomDetailDto = new BomDetailDto();
        rootBomDetailDto.Id = bom.Id;
        rootBomDetailDto.ProductId = bom.ProductId;
        rootBomDetailDto.ProductName = bom.Product.Name;
        rootBomDetailDto.Quantity = 0;

        //存放主产品Node
        Dictionary<DateTime, double> orders = new Dictionary<DateTime, double>();
        foreach (var item in mps.Details)
        {
            orders.Add(item.ProductionDate, item.Quantity);
        }
        int leadTime = 0;
        if (bom.Product.LeadTime != null)
        {
            leadTime = (int)bom.Product.LeadTime;
        }
        MrpNode rootNode = new MrpNode(
          bom.ProductId,
          bom.Product.Name,
          leadTime,
          0,
          orders,
          bom.Product.Number,
          bom.Product.Spec,
          bom.Product.ProductUnit.Name
          );


        nodeList.Add(rootNode);
        BomDetailDtoList.Add(rootBomDetailDto);


        //主产品的Bom明细
        List<BomDetail> rootBomDetailList = bom.Details;
        if (rootBomDetailList != null && rootBomDetailList.Count > 0)
        {
            foreach (var bomDetailTwo in rootBomDetailList)
            {
                BomDetailDto bomDetailDtoTwo = new BomDetailDto();
                bomDetailDtoTwo.Id = bomDetailTwo.Id;
                bomDetailDtoTwo.ProductId = bomDetailTwo.ProductId;
                bomDetailDtoTwo.ProductName = bomDetailTwo.Product.Name;
                bomDetailDtoTwo.Quantity = bomDetailTwo.Quantity;
                BomDetailDtoList.Add(bomDetailDtoTwo);


                int childLeadTime = 0;
                if (bomDetailTwo.Product.LeadTime != null)
                {
                    childLeadTime = (int)bomDetailTwo.Product.LeadTime;
                }
                MrpNode nodeTwo = new MrpNode(
                    bomDetailTwo.Product.Id,
                    bomDetailTwo.Product.Name,
                    childLeadTime,
                    0,
                    bomDetailTwo.Product.Number,
                    bomDetailTwo.Product.Spec,
                    bomDetailTwo.Product.ProductUnit.Name
                    );
                nodeList.Add(nodeTwo);

                MrpLine ml = new MrpLine(rootNode, nodeTwo, bomDetailTwo.Quantity);

                var queryable = await _bomRepository.WithDetailsAsync();
                queryable = queryable.Where(m => m.ProductId == bomDetailTwo.ProductId);
                var bomTwo = await AsyncExecuter.FirstOrDefaultAsync(queryable);


                if (bomTwo != null && bomTwo.Details != null && bomTwo.Details.Count() > 0)
                {
                    //需要将bom的id赋给detail的id,用于树形表格显示
                    bomDetailDtoTwo.Id = bomTwo.Id;
                    //递归
                    await SetBomDetailListAsync(BomDetailDtoList, bomDetailDtoTwo, nodeTwo, nodeList);
                }
            }
        }



        //数组
        MrpNode[] mrpNodes = new MrpNode[nodeList.Count()];
        int j = 0;
        foreach (var nl in nodeList)
        {
            mrpNodes[j] = nl;
            j++;
        }

        Graph g = new Graph(mrpNodes);
        MrpNode[] nodes = g.List();

        List<MrpDetail> mrpDetailList = new List<MrpDetail>();

        foreach (var node in nodes)
        {
            foreach (var workOrder in node.WorkOrders)
            {
                MrpDetail existsMrpDetail = mrpDetailList.Where(m =>
                    m.RequiredDate == workOrder.Key &&
                    m.ProductId == node.ProductId).FirstOrDefault();
                if (existsMrpDetail != null)
                {
                    existsMrpDetail.Quantity += workOrder.Value;
                }
                else
                {
                    MrpDetail mrpDetail = new MrpDetail(GuidGenerator.Create());
                    mrpDetail.RequiredDate = workOrder.Key;
                    mrpDetail.Quantity = workOrder.Value;
                    mrpDetail.ProductId = node.ProductId;
                    mrpDetailList.Add(mrpDetail);
                }
            }
        }

        mps.MrpDetails.Clear();
        mps.MrpDetails = mrpDetailList;
        await _repository.UpdateAsync(mps);
    }

    private async Task SetBomDetailListAsync(List<BomDetailDto> bomDetailDtoList, BomDetailDto bomDetailDto, MrpNode mrpNode, List<MrpNode> nodeList)
    {
        var queryable = await _bomRepository.WithDetailsAsync();
        queryable = queryable.Where(m => m.ProductId == bomDetailDto.ProductId);
        Bom bom = await AsyncExecuter.FirstOrDefaultAsync(queryable);

        if (bom == null) { return; }
        var bomDetailList = bom.Details;

        foreach (var subBomDetal in bomDetailList)
        {
            int leadTime = 0;
            if (subBomDetal.Product.LeadTime != null)
            {
                leadTime = (int)subBomDetal.Product.LeadTime;
            }
            MrpNode subNode = new MrpNode(
                subBomDetal.Product.Id,
                subBomDetal.Product.Name,
                leadTime,
                0,
                subBomDetal.Product.Number,
                subBomDetal.Product.Spec,
                subBomDetal.Product.ProductUnit.Name
                );
            nodeList.Add(subNode);


            BomDetailDto bomDetailDtoSub = new BomDetailDto();
            bomDetailDtoSub.Id = bomDetailDto.Id;
            bomDetailDtoSub.ProductName = subBomDetal.Product.Name;
            bomDetailDtoSub.ProductId = subBomDetal.Product.Id;
            bomDetailDtoSub.Quantity = subBomDetal.Quantity;
            bomDetailDtoList.Add(bomDetailDtoSub);


            MrpLine ml = new MrpLine(mrpNode, subNode, subBomDetal.Quantity);


            bomDetailDtoSub.Id = bom.Id;
            await SetBomDetailListAsync(bomDetailDtoList, bomDetailDtoSub, subNode, nodeList);
            //var queryableChild = await _bomRepository.WithDetailsAsync();
            //queryable = queryable.Where(m => m.ProductId == bomDetailDtoSub.ProductId);
            //Bom bomChild = await AsyncExecuter.FirstOrDefaultAsync(queryable);

            //if (bomChild != null && bomChild.Details != null && bomChild.Details.Count() > 0)
            //{

            //}
        }
    }


    [Authorize(ERPPermissions.Mps.Profile)]
    public async Task<MpsProfileDto> GetProfileAsync(Guid id)
    {
        Mps mps = await _repository.FindAsync(id);

        MpsProfileDto mpsProfileDto = ObjectMapper.Map<Mps, MpsProfileDto>(mps);

        return mpsProfileDto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    /// 
    [Authorize(ERPPermissions.Mps.Profile)]
    public async Task CreatePurchaseApplyWorkOrderAsync(CreatePurchaseApplyWorkOrderByMrpInput input)
    {
        Mps mps = await _repository.FindAsync(input.MpsId);
        PurchaseApply purchaseApply = new PurchaseApply(GuidGenerator.Create());
        purchaseApply.ApplyType = PurchaseApplyType.MPS;
        purchaseApply.MpsId = input.MpsId;
        List<WorkOrder> workOrderList = new List<WorkOrder>();

        List<MrpDetail> mrpDetailList = new List<MrpDetail>();

        if (input.MrpDetailIdList.Count >= 0)
        {
            foreach (var item in input.MrpDetailIdList)
            {
                var detail = mps.MrpDetails.FirstOrDefault(m => m.Id == item);
                if (detail != null)
                {
                    mrpDetailList.Add(detail);
                }
            }
        }
        else
        {
            mrpDetailList.AddRange(mps.MrpDetails);
        }

        for (int i = 0; i < mrpDetailList.Count; i++)
        {
            var mrpDetail = mrpDetailList[i];
            if (mrpDetail.Product.SourceType != BasicData.Products.ProductSourceType.Self)
            {

                var exsistsApply = purchaseApply.Details.Where(m=>m.ProductId == mrpDetail.ProductId).FirstOrDefault();

                if (exsistsApply == null)
                {
                    PurchaseApplyDetail purchaseApplyDetail = new PurchaseApplyDetail(GuidGenerator.Create());
                    purchaseApplyDetail.PurchaseApplyId = purchaseApply.Id;
                    purchaseApplyDetail.ProductId = mrpDetail.ProductId;
                    purchaseApplyDetail.Quantity = mrpDetail.Quantity; ;
                    purchaseApplyDetail.ArrivalDate = mrpDetail.RequiredDate;
                    purchaseApplyDetail.Sort = i;

                    purchaseApply.Details.Add(purchaseApplyDetail);
                }
                else
                {
                    exsistsApply.Quantity = exsistsApply.Quantity + mrpDetail.Quantity  ;

                    if (exsistsApply.ArrivalDate > mrpDetail.RequiredDate)
                    {
                        exsistsApply.ArrivalDate = mrpDetail.RequiredDate;
                    }
                }
            }
            else
            {
                WorkOrder workOrder = new WorkOrder(GuidGenerator.Create());
                string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.WorkOrderPrefix);
                workOrder.Number = number;
                workOrder.MpsId = mrpDetail.MpsId;
                workOrder.ProductId = mrpDetail.ProductId;
                workOrder.Quantity = mrpDetail.Quantity;
                workOrder.StartDate = mrpDetail.RequiredDate;
                workOrder.CompletionDate = mrpDetail.RequiredDate.AddDays(mrpDetail.Product.LeadTime ?? 0);
                if (mrpDetail.Product.DefaultWorkshopId != null)
                {
                    workOrder.WorkshopId = mrpDetail.Product.DefaultWorkshopId;
                }
                workOrder.StandardMaterialDetails = await _workOrderManager.GetBomDetailAsync(workOrder);
                workOrderList.Add(workOrder);
            }
        }

        if (purchaseApply.Details.Count > 0)
        {
            string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchaseApplyPrefix);
            purchaseApply.Number = number;
            mps.PurchaseApply = purchaseApply;
            await _repository.UpdateAsync(mps);
        }
        

        if (workOrderList.Count >= 0)
        {
            await _workOrderRepository.InsertManyAsync(workOrderList);
        }

    }
}
