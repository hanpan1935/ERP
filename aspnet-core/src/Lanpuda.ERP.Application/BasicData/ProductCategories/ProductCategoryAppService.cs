using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Lanpuda.ERP.Utils.UniqueCode;
using Volo.Abp.Domain.Repositories;
using Lanpuda.ERP.BasicData.Products;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.BasicData.ProductCategories;

[Authorize]
public class ProductCategoryAppService : ERPAppService, IProductCategoryAppService
{
    //protected override string GetPolicyName { get; set; } = ERPPermissions.ProductCategory.Default;
    //protected override string GetListPolicyName { get; set; } = ERPPermissions.ProductCategory.Default;
    //protected override string CreatePolicyName { get; set; } = ERPPermissions.ProductCategory.Create;
    //protected override string UpdatePolicyName { get; set; } = ERPPermissions.ProductCategory.Update;
    //protected override string DeletePolicyName { get; set; } = ERPPermissions.ProductCategory.Delete;

    private readonly IProductCategoryRepository _repository;
    private readonly IProductRepository _productRepository;

    public ProductCategoryAppService(IProductCategoryRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }


    [Authorize(ERPPermissions.ProductCategory.Create)]
    public async Task CreateAsync(ProductCategoryCreateDto input)
    {
        //Name 不能重复
        var hasSameName = await _repository.AnyAsync(m => m.Name == input.Name);
        if (hasSameName) { throw new UserFriendlyException("名称不能重复"); }

        Guid id = GuidGenerator.Create();
        ProductCategory productCategory = new ProductCategory(id, input.Name, input.Number, input.Remark);
        ProductCategory result = await _repository.InsertAsync(productCategory);
    }


    [Authorize(ERPPermissions.ProductCategory.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        ProductCategory productCategory = await _repository.FindAsync(id);
        if (productCategory == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        var isExsits = await _productRepository.AnyAsync(m => m.ProductCategoryId == id);
        if (isExsits) { throw new UserFriendlyException("请先删除分类下的产品"); }
        await _repository.DeleteAsync(productCategory);
    }


    [Authorize(ERPPermissions.ProductCategory.Update)]
    public async Task<ProductCategoryDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<ProductCategory, ProductCategoryDto>(result);
    }


    [Authorize(ERPPermissions.ProductCategory.Default)]
    public async Task<PagedResultDto<ProductCategoryDto>> GetPagedListAsync(ProductCategoryPagedRequestDto input)
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
        return new PagedResultDto<ProductCategoryDto>(totalCount, ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryDto>>(result));
    }


    [Authorize(ERPPermissions.ProductCategory.Update)]
    public async Task UpdateAsync(Guid id, ProductCategoryUpdateDto input)
    {
        ProductCategory productCategory = await _repository.FindAsync(id);
        if (productCategory == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

       
        //Name 不能重复
        var hasSameName = await _repository.AnyAsync(m => m.Name == input.Name && m.Id != id);
        if (hasSameName) { throw new UserFriendlyException("名称不能重复"); }

        productCategory.Name = input.Name;
        productCategory.Number = input.Number;
        productCategory.Remark = input.Remark;
        var result = await _repository.UpdateAsync(productCategory);
    }


    /// <summary>
    /// 查询所有分类
    /// 创建，编辑Product的时候用到
    /// </summary>
    /// <returns></returns>
    public async Task<List<ProductCategoryDto>> GetAllAsync()
    {
        var result = await _repository.GetListAsync();
        return ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryDto>>(result);
    }
}
