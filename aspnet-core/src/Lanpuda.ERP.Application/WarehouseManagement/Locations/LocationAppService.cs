using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.Locations;


[Authorize]
public class LocationAppService : ERPAppService, ILocationAppService
{
    private readonly ILocationRepository _repository;

    public LocationAppService(ILocationRepository repository)
    {
        _repository = repository;
    }


    [Authorize(ERPPermissions.Location.Create)]
    public async Task CreateAsync(LocationCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        Location location = new Location(id);
        location.WarehouseId = input.WarehouseId;
        location.Number = input.Number;
        location.Name = input.Name;
        location.Remark = input.Remark;
        Location result = await _repository.InsertAsync(location);
    }



    [Authorize(ERPPermissions.Location.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Location location = await _repository.FindAsync(id);
        if (location == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _repository.DeleteAsync(location);
    }

    [Authorize(ERPPermissions.Location.Update)]
    public async Task<LocationDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<Location, LocationDto>(result);
    }

    [Authorize(ERPPermissions.Location.Default)]
    public async Task<PagedResultDto<LocationDto>> GetPagedListAsync(LocationPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Name), m => m.Name.Contains(input.Name))
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.WarehouseId != null, m => m.WarehouseId.Equals(input.WarehouseId))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<LocationDto>(totalCount, ObjectMapper.Map<List<Location>, List<LocationDto>>(result));
    }

    [Authorize(ERPPermissions.Location.Update)]
    public async Task UpdateAsync(Guid id, LocationUpdateDto input)
    {
        Location location = await _repository.FindAsync(id);
        if (location == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        location.WarehouseId = input.WarehouseId;
        location.Number = input.Number;
        location.Name = input.Name;
        location.Remark = input.Remark;
        var result = await _repository.UpdateAsync(location);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<LocationDto>> GetByWarehouseIdAsync(Guid warehouseId)
    {
        var result = await _repository.GetListAsync(m => m.WarehouseId == warehouseId,true);
        return ObjectMapper.Map<List<Location>, List<LocationDto>>(result);
    }
}
