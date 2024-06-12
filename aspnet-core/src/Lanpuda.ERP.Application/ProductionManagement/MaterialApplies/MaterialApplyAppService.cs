using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.Utils.UniqueCode;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Volo.Abp.Data;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies;

[Authorize]
public class MaterialApplyAppService : ERPAppService, IMaterialApplyAppService
{
    private readonly IMaterialApplyRepository _repository;
    private readonly IMaterialApplyDetailRepository _detailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IWorkOrderOutRepository _workOrderOutRepository;

    public MaterialApplyAppService(
        IMaterialApplyRepository repository,
        IMaterialApplyDetailRepository detailRepository,
        IWorkOrderOutRepository workOrderOutRepository,
        IUniqueCodeUtils uniqueCodeUtils)
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _workOrderOutRepository = workOrderOutRepository;
    }

    [Authorize(ERPPermissions.MaterialApply.Create)]
    public async Task CreateAsync(MaterialApplyCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.MaterialApplyPrefix); ;
        MaterialApply materialApply = new MaterialApply(id);
        materialApply.Number = number;
        materialApply.Remark = input.Remark;
        materialApply.IsConfirmed = false;
        materialApply.WorkOrderId = input.WorkOrderId;


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            MaterialApplyDetail detail = new MaterialApplyDetail(detailId);
            detail.MaterialApplyId = id;
            detail.ProductId = item.ProductId;
            detail.Quantity = item.Quantity;
            detail.StandardQuantity = item.StandardQuantity;
            detail.Sort = i;
            materialApply.Details.Add(detail);
        }
       

        MaterialApply result = await _repository.InsertAsync(materialApply);
    }



    [Authorize(ERPPermissions.MaterialApply.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        MaterialApply materialApply = await _repository.FindAsync(id,true);
        if (materialApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }


        foreach (var item in materialApply.Details)
        {
            var workOrderOut = await _workOrderOutRepository.FirstOrDefaultAsync(x => x.MaterialApplyDetailId == item.Id);
            if (workOrderOut != null)
            {
                if (workOrderOut.IsSuccessful == true)
                {
                    throw new UserFriendlyException("已经入库无法删除!");
                }
                await _workOrderOutRepository.DeleteAsync(workOrderOut);
            }
        }
        await _repository.DeleteAsync(materialApply);
    }


    [Authorize(ERPPermissions.MaterialApply.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        MaterialApply materialApply = await _repository.FindAsync(id);
        if (materialApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (materialApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认过了");
        }

        foreach (var item in materialApply.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }

        materialApply.IsConfirmed = true;
        materialApply.ConfirmedUserId = CurrentUser.Id;
        materialApply.ConfirmedTime = Clock.Now;

        //创建生产领料单
        foreach (var item in materialApply.Details)
        {
            WorkOrderOut workOrderOut = new WorkOrderOut(GuidGenerator.Create());
            workOrderOut.MaterialApplyDetailId = item.Id;
            workOrderOut.Number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.WorkOrderOutPrefix);
            workOrderOut.IsSuccessful = false;

            item.WorkOrderOut = workOrderOut;
        }
        await _repository.UpdateAsync(materialApply);
    }


    [Authorize(ERPPermissions.MaterialApply.Update)]
    public async Task<MaterialApplyDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        return ObjectMapper.Map<MaterialApply, MaterialApplyDto>(result);
    }


    [Authorize(ERPPermissions.MaterialApply.Default)]
    public async Task<PagedResultDto<MaterialApplyDto>> GetPagedListAsync(MaterialApplyPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.WorkOrderNumber), m => m.WorkOrder.Number.Contains(input.WorkOrderNumber))
            .WhereIf(!string.IsNullOrEmpty(input.MpsNumber), m => m.WorkOrder.Mps.Number.Contains(input.MpsNumber))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.WorkOrder.Product.Name.Contains(input.ProductName))
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed == (input.IsConfirmed))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<MaterialApplyDto>(totalCount, ObjectMapper.Map<List<MaterialApply>, List<MaterialApplyDto>>(result));
    }


    [Authorize(ERPPermissions.MaterialApply.Update)]
    public async Task UpdateAsync(Guid id, MaterialApplyUpdateDto input)
    {
        MaterialApply materialApply = await _repository.FindAsync(id);
        if (materialApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //
        if (materialApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认,无法修改!");
        }


        materialApply.Remark = input.Remark;
        materialApply.IsConfirmed = false;

        List<MaterialApplyDetail> createList = new List<MaterialApplyDetail>();
        List<MaterialApplyDetail> updateList = new List<MaterialApplyDetail>();
        List<MaterialApplyDetail> deleteList = new List<MaterialApplyDetail>();
        List<MaterialApplyDetail> dbList = await _detailRepository.GetListAsync(m => m.MaterialApplyId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                MaterialApplyDetail detail = new MaterialApplyDetail(detailId);
                detail.MaterialApplyId = id;
                detail.ProductId = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.StandardQuantity = item.StandardQuantity;
                detail.Remark = item.Remark;
                createList.Add(detail);
            }
            else //编辑
            {
                MaterialApplyDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.StandardQuantity = item.StandardQuantity;
                detail.Remark = item.Remark;
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
        List<MaterialApplyDetail> details = new List<MaterialApplyDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);



        var result = await _repository.UpdateAsync(materialApply);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }

    //TODO 判断是否重复
    private void CheckIsProductRepeat(List<MaterialApplyDetail> details)
    {
        var res = from m in details group m by m.ProductId;

        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += (item.Sort+1) + ",";
                }
                throw new UserFriendlyException("第:" + rowNumbers + 1 + "产品重复");
            }
        }
    }

}
