using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections;

[Authorize]
public class ArrivalInspectionAppService : ERPAppService, IArrivalInspectionAppService
{
    private readonly IArrivalInspectionRepository _repository;
    public ArrivalInspectionAppService(IArrivalInspectionRepository repository)
    {
        _repository = repository;
    }


    [Authorize(ERPPermissions.ArrivalInspection.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        ArrivalInspection arrivalInspection = await _repository.FindAsync(id);
        if (arrivalInspection == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (arrivalInspection.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认过了");
        }
        arrivalInspection.IsConfirmed = true;
        arrivalInspection.ConfirmedTime = Clock.Now;
        arrivalInspection.ConfirmeUserId = CurrentUser.Id;
        await _repository.UpdateAsync(arrivalInspection);
    }



    [Authorize(ERPPermissions.ArrivalInspection.Update)]
    public async Task<ArrivalInspectionDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        return ObjectMapper.Map<ArrivalInspection, ArrivalInspectionDto>(result);
    }



    [Authorize(ERPPermissions.ArrivalInspection.Default)]
    public async Task<PagedResultDto<ArrivalInspectionDto>> GetPagedListAsync(ArrivalInspectionPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ArrivalNoticeNumber), m => m.ArrivalNoticeDetail.ArrivalNotice.Number.Contains(input.ArrivalNoticeNumber))
            .WhereIf(!string.IsNullOrEmpty(input.PurchaseStorageNumber), m => m.ArrivalNoticeDetail.PurchaseStorage.Number.Contains(input.PurchaseStorageNumber))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name.Contains(input.ProductName))
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed.Equals(input.IsConfirmed))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<ArrivalInspectionDto>(totalCount, ObjectMapper.Map<List<ArrivalInspection>, List<ArrivalInspectionDto>>(result));
    }



    [Authorize(ERPPermissions.ArrivalInspection.Update)]
    public async Task UpdateAsync(Guid id, ArrivalInspectionUpdateDto input)
    {
        ArrivalInspection arrivalInspection = await _repository.FindAsync(id);
        if (arrivalInspection == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        arrivalInspection.BadQuantity = input.BadQuantity;
        arrivalInspection.Description = input.Description;

        var result = await _repository.UpdateAsync(arrivalInspection);
    }
    
}
