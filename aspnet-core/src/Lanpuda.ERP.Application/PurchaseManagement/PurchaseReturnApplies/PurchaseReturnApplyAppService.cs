using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
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

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;


[Authorize]
public class PurchaseReturnApplyAppService : ERPAppService, IPurchaseReturnApplyAppService
{
    private readonly IPurchaseReturnApplyRepository _repository;
    private readonly IPurchaseReturnApplyDetailRepository _detailRepository;
    private readonly IPurchaseReturnRepository _purchaseReturnRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;

    public PurchaseReturnApplyAppService(
        IPurchaseReturnApplyRepository repository,
        IPurchaseReturnApplyDetailRepository detailRepository,
        IPurchaseReturnRepository purchaseReturnRepository,
        IUniqueCodeUtils uniqueCodeUtils)
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _purchaseReturnRepository = purchaseReturnRepository;
    }


    [Authorize(ERPPermissions.PurchaseReturnApply.Confirm)]
    public async Task ConfirmAsync(Guid id)
    {
        PurchaseReturnApply purchaseReturnApply = await _repository.FindAsync(id);
        if (purchaseReturnApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (purchaseReturnApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认了,无法再次确认");
        }

        if (purchaseReturnApply.Details == null || purchaseReturnApply.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        foreach (var item in purchaseReturnApply.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }

        purchaseReturnApply.IsConfirmed = true;
        purchaseReturnApply.ConfirmedTime = Clock.Now;
        purchaseReturnApply.ConfirmeUserId = CurrentUser.Id;

        #region 创建采购退货出库单
        foreach (var item in purchaseReturnApply.Details)
        {
            Guid returnId = GuidGenerator.Create();
            string returnNumber = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchaseReturnPrefix);
            PurchaseReturn purchaseReturn = new PurchaseReturn(returnId);
            purchaseReturn.Number = returnNumber;
            purchaseReturn.PurchaseReturnApplyDetailId = item.Id;
            purchaseReturn.IsSuccessful = false;
            item.PurchaseReturn = purchaseReturn;
        }
        #endregion

        await _repository.UpdateAsync(purchaseReturnApply);
    }



    [Authorize(ERPPermissions.PurchaseReturnApply.Create)]
    public async Task CreateAsync(PurchaseReturnApplyCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchaseReturnApplyPrefix); ;
        PurchaseReturnApply purchaseReturnApply = new PurchaseReturnApply(id);
        purchaseReturnApply.Number = number;
        purchaseReturnApply.SupplierId = input.SupplierId;
        purchaseReturnApply.ReturnReason = input.ReturnReason;
        purchaseReturnApply.Description = input.Description;
        purchaseReturnApply.Remark = input.Remark;
        purchaseReturnApply.IsConfirmed = false;

        List<PurchaseReturnApplyDetail> details = new List<PurchaseReturnApplyDetail>();

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            PurchaseReturnApplyDetail detail = new PurchaseReturnApplyDetail(detailId);
            detail.PurchaseReturnApplyId = id;
            detail.PurchaseStorageDetailId = item.PurchaseStorageDetailId;
            detail.Quantity = item.Quantity;
            detail.Remark = item.Remark;
            detail.Sort = i;
            details.Add(detail);
        }
        CheckIsRepeat(details);


        PurchaseReturnApply result = await _repository.InsertAsync(purchaseReturnApply);
        await _detailRepository.InsertManyAsync(details);
    }



    [Authorize(ERPPermissions.PurchaseReturnApply.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        PurchaseReturnApply purchaseReturnApply = await _repository.FindAsync(id);
        if (purchaseReturnApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        foreach (var item in purchaseReturnApply.Details)
        {
            var pruchaseReturn = await _purchaseReturnRepository.FirstOrDefaultAsync(m => m.PurchaseReturnApplyDetailId == item.Id);
            if (pruchaseReturn != null)
            {
                if (pruchaseReturn.IsSuccessful == true)
                {
                    throw new UserFriendlyException("已经入库,无法删除!");
                }
                else
                {
                    await _purchaseReturnRepository.DeleteAsync(pruchaseReturn);
                }
            }
        }

        await _repository.DeleteAsync(purchaseReturnApply);
    }



    [Authorize(ERPPermissions.PurchaseReturnApply.Update)]
    public async Task<PurchaseReturnApplyDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        return ObjectMapper.Map<PurchaseReturnApply, PurchaseReturnApplyDto>(result);
    }



    [Authorize(ERPPermissions.PurchaseReturnApply.Default)]
    public async Task<PagedResultDto<PurchaseReturnApplyDto>> GetPagedListAsync(PurchaseReturnApplyPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<PurchaseReturnApplyDto>(totalCount, ObjectMapper.Map<List<PurchaseReturnApply>, List<PurchaseReturnApplyDto>>(result));
    }



    [Authorize(ERPPermissions.PurchaseReturnApply.Update)]
    public async Task UpdateAsync(Guid id, PurchaseReturnApplyUpdateDto input)
    {
        PurchaseReturnApply purchaseReturnApply = await _repository.FindAsync(id);
        if (purchaseReturnApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //


        //状态验证 只有未确认的才能编辑 IsConfirmed = false;
        if (purchaseReturnApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认，无法编辑");
        }

        purchaseReturnApply.SupplierId = input.SupplierId;
        purchaseReturnApply.ReturnReason = input.ReturnReason;
        purchaseReturnApply.Description = input.Description;
        purchaseReturnApply.Remark = input.Remark;
        purchaseReturnApply.IsConfirmed = false;

        List<PurchaseReturnApplyDetail> createList = new List<PurchaseReturnApplyDetail>();
        List<PurchaseReturnApplyDetail> updateList = new List<PurchaseReturnApplyDetail>();
        List<PurchaseReturnApplyDetail> deleteList = new List<PurchaseReturnApplyDetail>();
        List<PurchaseReturnApplyDetail> dbList = await _detailRepository.GetListAsync(m => m.PurchaseReturnApplyId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                PurchaseReturnApplyDetail detail = new PurchaseReturnApplyDetail(detailId);
                detail.PurchaseReturnApplyId = id;
                detail.PurchaseStorageDetailId = item.PurchaseStorageDetailId;
                detail.Quantity = item.Quantity;
                detail.Remark = item.Remark;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                PurchaseReturnApplyDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.PurchaseReturnApplyId = id;
                detail.PurchaseStorageDetailId = item.PurchaseStorageDetailId;
                detail.Quantity = item.Quantity;
                detail.Remark = item.Remark;
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
        List<PurchaseReturnApplyDetail> details = new List<PurchaseReturnApplyDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsRepeat(details);


        var result = await _repository.UpdateAsync(purchaseReturnApply);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }



    private void CheckIsRepeat(List<PurchaseReturnApplyDetail> details)
    {
        var res = from m in details group m by m.PurchaseStorageDetailId;

        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += (item.Sort + 1) + "行,";
                }
                throw new UserFriendlyException("第:" + rowNumbers + "重复");
            }
        }
    }
}
