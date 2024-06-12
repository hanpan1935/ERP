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
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using Lanpuda.ERP.QualityManagement.ArrivalInspections;
using Volo.Abp.Domain.Repositories;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices;


[Authorize]
public class ArrivalNoticeAppService : ERPAppService, IArrivalNoticeAppService
{
    private readonly IArrivalNoticeRepository _arrivalNoticeRepository;
    private readonly IArrivalNoticeDetailRepository _arrivalNoticeDetailRepository;
    private readonly IPurchaseStorageRepository _purchaseStorageRepository;
    private readonly IArrivalInspectionRepository _arrivalInspectionRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;

    public ArrivalNoticeAppService(
        IArrivalNoticeRepository repository,
        IArrivalNoticeDetailRepository detailRepository,
        IUniqueCodeUtils uniqueCodeUtils,
        IPurchaseStorageRepository purchaseStorageRepository,
        IArrivalInspectionRepository arrivalInspectionRepository
        )
    {
        _arrivalNoticeRepository = repository;
        _arrivalNoticeDetailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _purchaseStorageRepository = purchaseStorageRepository;
        _arrivalInspectionRepository = arrivalInspectionRepository;
    }


    [Authorize(ERPPermissions.ArrivalNotice.Confirm)]
    public async Task<ArrivalNoticeDto> ConfirmeAsync(Guid id)
    {
        var arrivalNotice = await _arrivalNoticeRepository.FindAsync(id, true);

        if (arrivalNotice == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (arrivalNotice.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认了");
        }

        if (arrivalNotice.Details == null || arrivalNotice.Details.Count == 0)
        {
            throw new UserFriendlyException("订单明细不能为空");
        }

        foreach (var item in arrivalNotice.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }



        arrivalNotice.IsConfirmed = true;
        arrivalNotice.ConfirmedTime = Clock.Now;
        arrivalNotice.ConfirmeUserId = CurrentUser.Id;


        //生成采购入库单
        foreach (var item in arrivalNotice.Details)
        {
            Guid purchaseStorageId = GuidGenerator.Create();
            PurchaseStorage purchaseStorage = new PurchaseStorage(purchaseStorageId);
            purchaseStorage.ArrivalNoticeDetailId = item.Id;
            purchaseStorage.Number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchaseStoragePrefix);
            item.PurchaseStorage = purchaseStorage;
        }


        //如果检验,生成来料检验单
        foreach (var item in arrivalNotice.Details)
        {
            if (item.PurchaseOrderDetail.Product.IsArrivalInspection == true)
            {
                Guid arrivalInspectionId = GuidGenerator.Create();
                string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.ArrivalInspectionPrefix);
                ArrivalInspection arrivalInspection = new ArrivalInspection(arrivalInspectionId);
                arrivalInspection.Number = number;
                arrivalInspection.ArrivalNoticeDetailId = item.Id;
                item.ArrivalInspection = arrivalInspection;
            }
        }

        await _arrivalNoticeRepository.UpdateAsync(arrivalNotice);
        var dto = ObjectMapper.Map<ArrivalNotice, ArrivalNoticeDto>(arrivalNotice);
        return dto;
    }



    [Authorize(ERPPermissions.ArrivalNotice.Create)]
    public async Task CreateAsync(ArrivalNoticeCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.ArrivalNoticePrefix); ;
        ArrivalNotice arrivalNotice = new ArrivalNotice(id);
        arrivalNotice.Number = number;
        arrivalNotice.ArrivalTime = input.ArrivalTime;
        arrivalNotice.Remark = input.Remark;
        arrivalNotice.IsConfirmed = false;

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            ArrivalNoticeDetail detail = new ArrivalNoticeDetail(detailId);
            detail.ArrivalNoticeId = id;
            detail.PurchaseOrderDetailId = item.PurchaseOrderDetailId;
            detail.Quantity = item.Quantity;
            detail.Sort = i;
            arrivalNotice.Details.Add(detail);
        }

        CheckIsRepeat(arrivalNotice.Details);

        ArrivalNotice result = await _arrivalNoticeRepository.InsertAsync(arrivalNotice);
    }


    [Authorize(ERPPermissions.ArrivalNotice.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        ArrivalNotice arrivalNotice = await _arrivalNoticeRepository.FindAsync(id);
        if (arrivalNotice == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }


        foreach (var item in arrivalNotice.Details)
        {
            var storage = await _purchaseStorageRepository.FirstOrDefaultAsync(m => m.ArrivalNoticeDetailId == item.Id);
            if (storage != null)
            {
                if (storage.IsSuccessful == true)
                {
                    throw new UserFriendlyException("已经入库,无法删除!");
                }
                else
                {
                    await _purchaseStorageRepository.DeleteAsync(storage);
                }
            }
        }
        await _arrivalNoticeRepository.DeleteAsync(arrivalNotice);
    }


    [Authorize(ERPPermissions.ArrivalNotice.Update)]
    public async Task<ArrivalNoticeDto> GetAsync(Guid id)
    {
        var result = await _arrivalNoticeRepository.FindAsync(id, true);
        var dto = ObjectMapper.Map<ArrivalNotice, ArrivalNoticeDto>(result);
        return dto;
    }


    [Authorize(ERPPermissions.ArrivalNotice.Default)]
    public async Task<PagedResultDto<ArrivalNoticeDto>> GetPagedListAsync(ArrivalNoticePagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _arrivalNoticeRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed.Equals(input.IsConfirmed))
            .WhereIf(input.ArrivalTimeStart != null, m => m.ArrivalTime >= (input.ArrivalTimeStart))
            .WhereIf(input.ArrivalTimeEnd != null, m => m.ArrivalTime <= (input.ArrivalTimeEnd))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<ArrivalNoticeDto>(totalCount, ObjectMapper.Map<List<ArrivalNotice>, List<ArrivalNoticeDto>>(result));
    }



    [Authorize(ERPPermissions.ArrivalNotice.Update)]
    public async Task UpdateAsync(Guid id, ArrivalNoticeUpdateDto input)
    {
        ArrivalNotice arrivalNotice = await _arrivalNoticeRepository.FindAsync(id);
        if (arrivalNotice == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //状态验证 只有未确认的才能编辑 IsConfirmed = false;
        if (arrivalNotice.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认无法编辑");
        }
        arrivalNotice.ArrivalTime = input.ArrivalTime;
        arrivalNotice.Remark = input.Remark;
        arrivalNotice.IsConfirmed = false;


        List<ArrivalNoticeDetail> createList = new List<ArrivalNoticeDetail>();
        List<ArrivalNoticeDetail> updateList = new List<ArrivalNoticeDetail>();
        List<ArrivalNoticeDetail> deleteList = new List<ArrivalNoticeDetail>();
        List<ArrivalNoticeDetail> dbList = await _arrivalNoticeDetailRepository.GetListAsync(m => m.ArrivalNoticeId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                ArrivalNoticeDetail detail = new ArrivalNoticeDetail(detailId);
                detail.ArrivalNoticeId = id;
                detail.PurchaseOrderDetailId = item.PurchaseOrderDetailId;
                detail.Quantity = item.Quantity;
                createList.Add(detail);
            }
            else //编辑
            {
                ArrivalNoticeDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ArrivalNoticeId = id;
                detail.PurchaseOrderDetailId = item.PurchaseOrderDetailId;
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
        List<ArrivalNoticeDetail> details = new List<ArrivalNoticeDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsRepeat(details);

        var result = await _arrivalNoticeRepository.UpdateAsync(arrivalNotice);
        await _arrivalNoticeDetailRepository.InsertManyAsync(createList);
        await _arrivalNoticeDetailRepository.UpdateManyAsync(updateList);
        await _arrivalNoticeDetailRepository.DeleteManyAsync(deleteList);
    }



    private void CheckIsRepeat(List<ArrivalNoticeDetail> details)
    {
        var res = from m in details group m by m.PurchaseOrderDetailId;

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
