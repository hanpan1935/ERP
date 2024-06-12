using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;
using Lanpuda.ERP.Utils.UniqueCode;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies;

[Authorize]
public class SalesReturnApplyAppService : ERPAppService, ISalesReturnApplyAppService
{
    private readonly ISalesReturnApplyRepository _repository;
    private readonly ISalesReturnApplyDetailRepository _detailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly ISalesReturnRepository _salesReturnRepository;

    public SalesReturnApplyAppService(
        ISalesReturnApplyRepository repository,
        ISalesReturnApplyDetailRepository detailRepository,
        ISalesReturnRepository salesReturnRepository,
        IUniqueCodeUtils uniqueCodeUtils)
    {

        _repository = repository;
        _detailRepository = detailRepository;
        _salesReturnRepository = salesReturnRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
    }


    [Authorize(ERPPermissions.SalesReturnApply.Create)]
    public async Task CreateAsync(SalesReturnApplyCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.SalesReturnApplyPrefix); ;
        SalesReturnApply salesReturnApply = new SalesReturnApply(id);
        salesReturnApply.Number = number;
        salesReturnApply.CustomerId = input.CustomerId;
        salesReturnApply.Reason = input.Reason;
        salesReturnApply.IsProductReturn = input.IsProductReturn;
        salesReturnApply.Description = input.Description;

        List<SalesReturnApplyDetail> details = new List<SalesReturnApplyDetail>();


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            SalesReturnApplyDetail detail = new SalesReturnApplyDetail(detailId);
            detail.SalesReturnApplyId = id;
            detail.SalesOutDetailId = item.SalesOutDetailId;
            detail.Quantity = item.Quantity;
            detail.Description = item.Description;
            detail.Sort = i;
            details.Add(detail);
        }

        CheckIsProductRepeat(details);
        SalesReturnApply result = await _repository.InsertAsync(salesReturnApply);
        await _detailRepository.InsertManyAsync(details);
    }


    [Authorize(ERPPermissions.SalesReturnApply.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        SalesReturnApply salesReturnApply = await _repository.FindAsync(id);
        if (salesReturnApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //��ѯ�˻���ⵥ

        foreach (var item in salesReturnApply.Details)
        {
            var salesReturn = await _salesReturnRepository.FirstOrDefaultAsync(m => m.SalesReturnApplyDetailId == item.Id);
            if (salesReturn != null)
            {
                if (salesReturn.IsSuccessful == false)
                {
                    await _salesReturnRepository.DeleteAsync(salesReturn);
                }
                else
                {
                    throw new UserFriendlyException("�Ѿ����,�޷�ɾ��");
                }
            }
        }
        await _repository.DeleteAsync(salesReturnApply);
    }

    [Authorize(ERPPermissions.SalesReturnApply.Update)]
    public async Task<SalesReturnApplyDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);

        return ObjectMapper.Map<SalesReturnApply, SalesReturnApplyDto>(result);
    }

    [Authorize(ERPPermissions.SalesReturnApply.Default)]
    public async Task<PagedResultDto<SalesReturnApplyDto>> GetPagedListAsync(SalesReturnApplyPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.CustomerId != null, m => m.CustomerId == input.CustomerId)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SalesReturnApplyDto>(totalCount, ObjectMapper.Map<List<SalesReturnApply>, List<SalesReturnApplyDto>>(result));
    }

    [Authorize(ERPPermissions.SalesReturnApply.Update)]
    public async Task UpdateAsync(Guid id, SalesReturnApplyUpdateDto input)
    {
        SalesReturnApply salesReturnApply = await _repository.FindAsync(id);
        if (salesReturnApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //״̬��֤ ֻ��δȷ�ϵĲ��ܱ༭ IsConfirmed = false;
        if (salesReturnApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("�Ѿ�ȷ�ϣ��޷��༭");
        }


        salesReturnApply.CustomerId = input.CustomerId;
        salesReturnApply.Reason = input.Reason;
        salesReturnApply.IsProductReturn = input.IsProductReturn;
        salesReturnApply.Description = input.Description;

        List<SalesReturnApplyDetail> createList = new List<SalesReturnApplyDetail>();
        List<SalesReturnApplyDetail> updateList = new List<SalesReturnApplyDetail>();
        List<SalesReturnApplyDetail> deleteList = new List<SalesReturnApplyDetail>();
        List<SalesReturnApplyDetail> dbList = await _detailRepository.GetListAsync(m => m.SalesReturnApplyId == id);


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //�½�
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                SalesReturnApplyDetail detail = new SalesReturnApplyDetail(detailId);
                detail.SalesReturnApplyId = id;
                detail.SalesOutDetailId = item.SalesOutDetailId;
                detail.Quantity = item.Quantity;
                detail.Description = item.Description;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //�༭
            {
                SalesReturnApplyDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.SalesReturnApplyId = id;
                detail.SalesOutDetailId = item.SalesOutDetailId;
                detail.Quantity = item.Quantity;
                detail.Description = item.Description;
                detail.Sort = i;
                updateList.Add(detail);
            }
        }


        //ɾ��
        foreach (var dbItem in dbList)
        {
            bool isExsiting = input.Details.Any(m => m.Id == dbItem.Id);
            if (isExsiting == false)
            {
                deleteList.Add(dbItem);
            }
        }


        //����
        List<SalesReturnApplyDetail> details = new List<SalesReturnApplyDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);


        var result = await _repository.UpdateAsync(salesReturnApply);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }

    [Authorize(ERPPermissions.SalesReturnApply.Confirm)]
    public async Task ConfirmAsync(Guid id)
    {
        SalesReturnApply apply = await _repository.FindAsync(id);
        if (apply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        if (apply.IsConfirmed == true)
        {
            throw new UserFriendlyException("�Ѿ�ȷ����");
        }

        if (apply.Details == null || apply.Details.Count == 0)
        {
            throw new UserFriendlyException("��ϸ����Ϊ��");
        }

        foreach (var item in apply.Details)
        {
            if (item.Quantity < 0)
            {
                throw new UserFriendlyException("��������Ϊ����");
            }
        }

        //��ϸ�����ظ�
        CheckIsProductRepeat(apply.Details);

        apply.IsConfirmed = true;
        apply.ConfirmeTime = Clock.Now;
        apply.ConfirmeUserId = CurrentUser.Id;

        //���������˻���ⵥ
        if (apply.IsProductReturn == true)
        {
            foreach (var item in apply.Details)
            {
                SalesReturn salesReturn = new SalesReturn(GuidGenerator.Create());
                string returnNumber = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.SalesOutPrefix);
                salesReturn.Number = returnNumber;
                salesReturn.SalesReturnApplyDetailId = item.Id;
                item.SalesReturn = salesReturn;
            }
        }
        await _repository.UpdateAsync(apply);
    }


    //TODO �ж��Ƿ��ظ�
    private void CheckIsProductRepeat(List<SalesReturnApplyDetail> details)
    {
        var res = from m in details group m by m.SalesOutDetailId;

        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += (item.Sort + 1) + "��,";
                }
                throw new UserFriendlyException("��:" + rowNumbers + "�ظ�");
            }
        }
    }
}
