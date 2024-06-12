using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.ProductUnits;
using Lanpuda.ERP.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Lanpuda.ERP.BasicData.Products;

[Authorize]
public class ProductAppService : ERPAppService, IProductAppService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IProductUnitRepository _productUnitRepository;

    public ProductAppService(
        IProductRepository productRepository,
        IProductCategoryRepository productCategoryRepository,
        IProductUnitRepository productUnitRepository
        )
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _productUnitRepository = productUnitRepository;
    }

    [Authorize(ERPPermissions.Product.Create)]
    public async Task CreateAsync(ProductCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        Product product = new Product(id);
        //product.Number = input.Number;
        //product.ProductCategoryId = input.ProductCategoryId;
        //product.ProductUnitId = input.ProductUnitId;
        //product.Name = input.Name;
        //product.Spec = input.Spec;
        //product.SourceType = input.SourceType;
        //product.Remark = input.Remark;
        //product.ProductionBatch = input.ProductionBatch;
        //product.DefaultLocationId = input.DefaultLocationId;
        //product.LeadTime = input.LeadTime;
        product = ObjectMapper.Map<ProductCreateDto, Product>(input);
        await _productRepository.InsertAsync(product);
    }


    [Authorize(ERPPermissions.Product.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Product product = await _productRepository.FindAsync(id);
        if (product == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _productRepository.DeleteAsync(product);
    }


    [Authorize(ERPPermissions.Product.Update)]
    public async Task<ProductDto> GetAsync(Guid id)
    {
        var result = await _productRepository.FindAsync(id);
        return ObjectMapper.Map<Product, ProductDto>(result);
    }


    [Authorize(ERPPermissions.Product.Default)]
    public async Task<PagedResultDto<ProductDto>> GetPagedListAsync(ProductPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _productRepository.WithDetailsAsync();

        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Name), m => m.Name.Contains(input.Name))
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.Spec), m => m.Spec.Contains(input.Spec))
            .WhereIf(input.ProductCategoryId != null, m => m.ProductCategoryId.Equals(input.ProductCategoryId))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<ProductDto>(totalCount, ObjectMapper.Map<List<Product>, List<ProductDto>>(result));
    }


    [Authorize(ERPPermissions.Product.Update)]
    public async Task UpdateAsync(Guid id, ProductUpdateDto input)
    {
        Product product = await _productRepository.FindAsync(id);
        if (product == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        product.Number = input.Number; ;
        product.ProductCategoryId = input.ProductCategoryId;
        product.ProductUnitId = input.ProductUnitId;
        product.Name = input.Name;
        product.Spec = input.Spec;
        product.SourceType = input.SourceType;
        product.ProductionBatch= input.ProductionBatch;
        product.DefaultLocationId = input.DefaultLocationId;
        product.LeadTime = input.LeadTime;
        product.Remark = input.Remark;
        product.IsArrivalInspection= input.IsArrivalInspection;
        product.IsProcessInspection = input.IsProcessInspection;
        product.IsFinalInspection = input.IsFinalInspection;
        product.DefaultWorkshopId = input.DefaultWorkshopId;
        var result = await _productRepository.UpdateAsync(product);
    }
}
