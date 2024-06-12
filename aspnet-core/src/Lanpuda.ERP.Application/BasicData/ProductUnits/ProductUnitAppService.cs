using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Lanpuda.ERP.BasicData.Products;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.BasicData.ProductUnits;

[Authorize]
public class ProductUnitAppService : ERPAppService ,IProductUnitAppService
{
    private readonly IProductUnitRepository _repository;
    private readonly IProductRepository _productRepository;

    public ProductUnitAppService(IProductUnitRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }


    [Authorize(ERPPermissions.ProductUnit.Create)]
    public async Task CreateAsync(ProductUnitCreateDto input)
    {
        //Name 不能重复
        var hasSameName = await _repository.AnyAsync(m => m.Name == input.Name);
        if (hasSameName) { throw new UserFriendlyException("名称不能重复"); }


        Guid id = GuidGenerator.Create();
        ProductUnit productUnit = new ProductUnit(id, input.Name,input.Number,input.Remark);
        ProductUnit result = await _repository.InsertAsync(productUnit);
    }

    [Authorize(ERPPermissions.ProductUnit.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        ProductUnit productUnit = await _repository.FindAsync(id);
        if (productUnit == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        var isExsist = await _productRepository.AnyAsync(m=>m.ProductUnitId == productUnit.Id);
        if (isExsist)
        {
            throw new UserFriendlyException("请先删除对应的产品");
        }
        await _repository.DeleteAsync(productUnit);
    }


    [Authorize(ERPPermissions.ProductUnit.Update)]
    public async Task<ProductUnitDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<ProductUnit, ProductUnitDto>(result);
    }


    [Authorize(ERPPermissions.ProductUnit.Default)]
    public async Task<PagedResultDto<ProductUnitDto>> GetPagedListAsync(ProductUnitPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query.WhereIf(!string.IsNullOrEmpty(input.Name), m => m.Name.Contains(input.Name));
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<ProductUnitDto>(totalCount, ObjectMapper.Map<List<ProductUnit>, List<ProductUnitDto>>(result));
    }


    [Authorize(ERPPermissions.ProductUnit.Update)]
    public async Task UpdateAsync(Guid id, ProductUnitUpdateDto input)
    {
        ProductUnit productUnit = await _repository.FindAsync(id);
        if (productUnit == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

       
        //Name 不能重复
        var hasSameName = await _repository.AnyAsync(m => m.Name == input.Name && m.Id != id);
        if (hasSameName) { throw new UserFriendlyException("名称不能重复"); }


        productUnit.Name = input.Name;
        productUnit.Number = input.Number;
        productUnit.Remark = input.Remark;
        var result = await _repository.UpdateAsync(productUnit);
    }


    /// <summary>
    /// 获取所有单位
    /// 新建，编辑Product的时候用到
    /// </summary>
    /// <returns></returns>
    public async Task<List<ProductUnitDto>> GetAllAsync()
    {
        var result = await _repository.GetListAsync();
        return ObjectMapper.Map<List<ProductUnit>, List<ProductUnitDto>>(result);
    }

}
