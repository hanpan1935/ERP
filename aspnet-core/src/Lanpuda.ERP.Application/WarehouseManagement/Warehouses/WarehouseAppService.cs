using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses;



[Authorize]
public class WarehouseAppService : ERPAppService ,IWarehouseAppService
{
    //protected override string GetPolicyName { get; set; } = ERPPermissions.Warehouse.Default;
    //protected override string GetListPolicyName { get; set; } = ERPPermissions.Warehouse.Default;
    //protected override string CreatePolicyName { get; set; } = ERPPermissions.Warehouse.Create;
    //protected override string UpdatePolicyName { get; set; } = ERPPermissions.Warehouse.Update;
    //protected override string DeletePolicyName { get; set; } = ERPPermissions.Warehouse.Delete;

    private readonly IWarehouseRepository _repository;

    public WarehouseAppService(IWarehouseRepository repository)
    {
        _repository = repository;
    }



    [Authorize(ERPPermissions.Warehouse.Create)]
    public async Task CreateAsync(WarehouseCreateDto input)
    {
        //TODO 全称不能重复

        //TODO 简称不能重复

        Guid id = GuidGenerator.Create();
        Warehouse warehouse = new Warehouse(id);
        warehouse.Number = input.Number;
        warehouse.Name = input.Name;
        warehouse.Remark = input.Remark;
        Warehouse result = await _repository.InsertAsync(warehouse);
    }



    [Authorize(ERPPermissions.Warehouse.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Warehouse warehouse = await _repository.FindAsync(id,true);
        if (warehouse == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        if (warehouse.Locations !=null || warehouse.Locations.Count > 0)
        {
            throw new UserFriendlyException("请先删除库位");
        }
        await _repository.DeleteAsync(warehouse);
    }



    [Authorize(ERPPermissions.Warehouse.Update)]
    public async Task<WarehouseDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<Warehouse, WarehouseDto>(result);
    }



    [Authorize(ERPPermissions.Warehouse.Default)]
    public async Task<PagedResultDto<WarehouseDto>> GetPagedListAsync(WarehousePagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Name), m => m.Name.Contains(input.Name))
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<WarehouseDto>(totalCount, ObjectMapper.Map<List<Warehouse>, List<WarehouseDto>>(result));
    }



    [Authorize(ERPPermissions.Warehouse.Update)]
    public async Task UpdateAsync(Guid id, WarehouseUpdateDto input)
    {
        //TODO 全称不能重复

        //TODO 简称不能重复
        Warehouse warehouse = await _repository.FindAsync(id);
        if (warehouse == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        warehouse.Number = input.Number;
        warehouse.Name = input.Name;
        warehouse.Remark = input.Remark;
        var result = await _repository.UpdateAsync(warehouse);
    }

    [Authorize]
    public async Task<List<WarehouseDto>> GetAllAsync()
    {
        var result = await _repository.GetListAsync(true);

        result = result.OrderBy(m => m.CreationTime).ToList();

        return ObjectMapper.Map<List<Warehouse>, List<WarehouseDto>>(result);
    }
}
