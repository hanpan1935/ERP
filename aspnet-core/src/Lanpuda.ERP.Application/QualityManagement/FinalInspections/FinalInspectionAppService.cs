using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.QualityManagement.FinalInspections;

[Authorize]
public class FinalInspectionAppService : ERPAppService, IFinalInspectionAppService
{

    private readonly IFinalInspectionRepository _finalInspectionRepository;

    public FinalInspectionAppService(IFinalInspectionRepository finalInspectionRepository)
    {
        _finalInspectionRepository = finalInspectionRepository;
    }



    [Authorize(ERPPermissions.FinalInspection.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        FinalInspection finalInspection = await _finalInspectionRepository.FindAsync(id);
        if (finalInspection == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (finalInspection.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认过了");
        }
        finalInspection.IsConfirmed = true;
        finalInspection.ConfirmedTime = Clock.Now;
        finalInspection.ConfirmeUserId = CurrentUser.Id;
        await _finalInspectionRepository.UpdateAsync(finalInspection);
    }



    [Authorize(ERPPermissions.FinalInspection.Update)]
    public async Task<FinalInspectionDto> GetAsync(Guid id)
    {
        var result = await _finalInspectionRepository.FindAsync(id);
        return ObjectMapper.Map<FinalInspection, FinalInspectionDto>(result);
    }

    [Authorize(ERPPermissions.FinalInspection.Default)]
    public async Task<PagedResultDto<FinalInspectionDto>> GetPagedListAsync(FinalInspectionPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _finalInspectionRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrEmpty(), x => x.Number.Contains(input.Number))
            .WhereIf(!input.MpsNumber.IsNullOrEmpty(), x => x.Mps.Number.Contains(input.MpsNumber))
            .WhereIf(!input.ProductName.IsNullOrEmpty(), x => x.Mps.Product.Name.Contains(input.ProductName) )
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<FinalInspectionDto>(totalCount, ObjectMapper.Map<List<FinalInspection>, List<FinalInspectionDto>>(result));
    }


    [Authorize(ERPPermissions.FinalInspection.Update)]
    public async Task UpdateAsync(Guid id, FinalInspectionUpdateDto input)
    {
        FinalInspection finalInspection = await _finalInspectionRepository.FindAsync(id);
        if (finalInspection == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        finalInspection.BadQuantity = input.BadQuantity;
        finalInspection.Description = input.Description;
        var result = await _finalInspectionRepository.UpdateAsync(finalInspection);
    }
}
