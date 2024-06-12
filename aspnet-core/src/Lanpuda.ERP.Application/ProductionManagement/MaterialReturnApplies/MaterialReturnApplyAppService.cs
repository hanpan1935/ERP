using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;
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

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;

[Authorize]
public class MaterialReturnApplyAppService : ERPAppService, IMaterialReturnApplyAppService
{
    private readonly IMaterialReturnApplyRepository _repository;
    private readonly IMaterialReturnApplyDetailRepository _detailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IWorkOrderReturnRepository _workOrderReturnRepository;

    public MaterialReturnApplyAppService(
        IMaterialReturnApplyRepository repository,
        IMaterialReturnApplyDetailRepository detailRepository,
        IUniqueCodeUtils uniqueCodeUtils,
        IWorkOrderReturnRepository workOrderReturnRepository)
    {

        _repository = repository;
        _detailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _workOrderReturnRepository = workOrderReturnRepository;
    }


    [Authorize(ERPPermissions.MaterialReturnApply.Create)]
    public async Task CreateAsync(MaterialReturnApplyCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.MaterialReturnApplyPrefix); 
        MaterialReturnApply materialReturnApply = new MaterialReturnApply(id);
        materialReturnApply.Number = number;
        materialReturnApply.Remark = input.Remark;
        materialReturnApply.IsConfirmed = false;

        List<MaterialReturnApplyDetail> details = new List<MaterialReturnApplyDetail>();

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            MaterialReturnApplyDetail detail = new MaterialReturnApplyDetail(detailId);
            detail.MaterialReturnApplyId = id;
            detail.WorkOrderOutDetailId = item.WorkOrderOutDetailId;
            detail.Quantity = item.Quantity;
            detail.Sort = i;
            details.Add(detail);
        }


        CheckIsRepeat(details);

        MaterialReturnApply result = await _repository.InsertAsync(materialReturnApply);
        await _detailRepository.InsertManyAsync(details);
    }

    [Authorize(ERPPermissions.MaterialReturnApply.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        MaterialReturnApply materialReturnApply = await _repository.FindAsync(id);
        if (materialReturnApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }


        foreach (var item in materialReturnApply.Details)
        {
            var workOrderReturn = await _workOrderReturnRepository.FirstOrDefaultAsync(m => m.MaterialReturnApplyDetailId == item.Id);
            if (workOrderReturn != null)
            {
                if (workOrderReturn.IsSuccessful == true)
                {
                    throw new UserFriendlyException("已经入库,无法删除");
                }
                await _workOrderReturnRepository.DeleteAsync(workOrderReturn);
            }
        }
        await _repository.DeleteAsync(materialReturnApply);
    }

    [Authorize(ERPPermissions.MaterialReturnApply.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        MaterialReturnApply materialReturnApply = await _repository.FindAsync(id);
        if (materialReturnApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (materialReturnApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认过了");
        }

        foreach (var item in materialReturnApply.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }

        materialReturnApply.IsConfirmed = true;
        materialReturnApply.ConfirmedUserId = CurrentUser.Id;
        materialReturnApply.ConfirmedTime = Clock.Now;
        //创建生产退料单
        foreach (var item in materialReturnApply.Details)
        {
            WorkOrderReturn workOrderReturn = new WorkOrderReturn(GuidGenerator.Create());
            workOrderReturn.Number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.WorkOrderReturnPrefix);
            workOrderReturn.MaterialReturnApplyDetailId = item.Id;
            workOrderReturn.IsSuccessful = false;

            item.WorkOrderReturn = workOrderReturn;
        }
        await _repository.UpdateAsync(materialReturnApply);
    }

    [Authorize(ERPPermissions.MaterialReturnApply.Update)]
    public async Task<MaterialReturnApplyDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        return ObjectMapper.Map<MaterialReturnApply, MaterialReturnApplyDto>(result);
    }

    [Authorize(ERPPermissions.MaterialReturnApply.Default)]
    public async Task<PagedResultDto<MaterialReturnApplyDto>> GetPagedListAsync(MaterialReturnApplyPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed.Equals(input.IsConfirmed))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<MaterialReturnApplyDto>(totalCount, ObjectMapper.Map<List<MaterialReturnApply>, List<MaterialReturnApplyDto>>(result));
    }

    [Authorize(ERPPermissions.MaterialReturnApply.Update)]
    public async Task UpdateAsync(Guid id, MaterialReturnApplyUpdateDto input)
    {
        MaterialReturnApply materialReturnApply = await _repository.FindAsync(id);
        if (materialReturnApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //
        if (materialReturnApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认,无法修改!");
        }

        materialReturnApply.Remark = input.Remark;
        materialReturnApply.IsConfirmed = false;

        List<MaterialReturnApplyDetail> createList = new List<MaterialReturnApplyDetail>();
        List<MaterialReturnApplyDetail> updateList = new List<MaterialReturnApplyDetail>();
        List<MaterialReturnApplyDetail> deleteList = new List<MaterialReturnApplyDetail>();
        List<MaterialReturnApplyDetail> dbList = await _detailRepository.GetListAsync(m => m.MaterialReturnApplyId == id);


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item  = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                MaterialReturnApplyDetail detail = new MaterialReturnApplyDetail(detailId);
                detail.MaterialReturnApplyId = id;
                detail.WorkOrderOutDetailId = item.WorkOrderOutDetailId;
                detail.Quantity = item.Quantity;
                createList.Add(detail);
            }
            else //编辑
            {
                MaterialReturnApplyDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.WorkOrderOutDetailId = item.WorkOrderOutDetailId;
                detail.Quantity = item.Quantity;
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
        List<MaterialReturnApplyDetail> details = new List<MaterialReturnApplyDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsRepeat(details);

        var result = await _repository.UpdateAsync(materialReturnApply);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }


    //TODO 判断是否重复


    private void CheckIsRepeat(List<MaterialReturnApplyDetail> details)
    {
        var res = from m in details group m by m.WorkOrderOutDetailId;

        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += (item.Sort + 1) + "";
                }
                throw new UserFriendlyException("第:" + rowNumbers + "重复");
            }
        }
    }
}
