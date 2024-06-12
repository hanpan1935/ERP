using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices;
using Lanpuda.ERP.SalesManagement.SalesPrices;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.BasicData.Products
{
    [Authorize]
    public class ProductLookupAppService : ERPAppService, IProductLookupAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPurchasePriceRepository _purchasePriceRepository;
        private readonly ISalesPriceRepository _salesPriceRepository;
        public ProductLookupAppService(
            IProductRepository productRepository,
            IPurchasePriceRepository purchasePriceRepository, 
            ISalesPriceRepository salesPriceRepository)
        {
            _productRepository = productRepository;
            _purchasePriceRepository = purchasePriceRepository;
            _salesPriceRepository = salesPriceRepository;
        }


        public async Task<List<ProductDto>> GetAllAsync()
        {
            var result = await _productRepository.GetListAsync(true);
            return ObjectMapper.Map<List<Product>, List<ProductDto>>(result);
        }


        public async Task<List<ProductDto>> GetForMpsAsync()
        {
            var queryable = await _productRepository.GetQueryableAsync();
            queryable = queryable.Where(m => m.SourceType == ProductSourceType.Self);
            var result = await AsyncExecuter.ToListAsync(queryable);
            return ObjectMapper.Map<List<Product>, List<ProductDto>>(result);
        }


        public async Task<PagedResultDto<ProductDto>> GetPagedListForWorkOrderAsync(ProductPagedRequestDto input)
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


        public async Task<PagedResultDto<ProductWithPriceDto>> GetPagedListWithPurchasePriceAsync(ProductWithPurchasePricePagedRequestDto input)
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

           
            var resultDtoList = new List<ProductWithPriceDto>();
            //查询采购报价(取最近的一次报价单）
            var queryable = await _purchasePriceRepository.WithDetailsAsync();
           
            foreach (var item in result)
            {
                ProductWithPriceDto resultDto = new ProductWithPriceDto();
                resultDto.Id = item.Id;
                resultDto.Number = item.Number;
                if (resultDto.ProductCategoryId != null)
                {
                    resultDto.ProductCategoryId = item.ProductCategoryId;
                    resultDto.ProductCategoryName = item.ProductCategory.Name;
                }
                resultDto.ProductUnitId = item.ProductUnitId;
                resultDto.ProductUnitName = item.ProductUnit.Name;
                resultDto.Name = item.Name;
                resultDto.Spec = item.Spec;
                resultDto.SourceType = item.SourceType;
                resultDto.Price = null;
                var now = Clock.Now;
                var purchasePrice = queryable.Where(m => m.SupplierId == input.SupplierId ).OrderByDescending(m => m.QuotationDate).FirstOrDefault();
                if (purchasePrice != null)
                {
                    var purchasePriceDetail = purchasePrice.Details.Where(m => m.ProductId == item.Id).FirstOrDefault();
                    if (purchasePriceDetail != null)
                    {
                        resultDto.Price = purchasePriceDetail.Price;
                        resultDto.TaxRate = purchasePriceDetail.TaxRate;
                    }
                }
               
                resultDtoList.Add(resultDto);
            }

            return new PagedResultDto<ProductWithPriceDto>(totalCount, resultDtoList);
        }


        public async Task<PagedResultDto<ProductWithPriceDto>> GetPagedListWithSalesPriceAsync(ProductWithSalesPricePagedRequestDto input)
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


            var resultDtoList = new List<ProductWithPriceDto>();
            //查询采购报价(取最近的一次报价单）
            var queryable = await _salesPriceRepository.WithDetailsAsync();

            foreach (var item in result)
            {
                ProductWithPriceDto resultDto = new ProductWithPriceDto();
                resultDto.Id = item.Id;
                resultDto.Number = item.Number;
                if (item.ProductCategoryId != null)
                {
                    resultDto.ProductCategoryId = item.ProductCategoryId;
                    resultDto.ProductCategoryName = item.ProductCategory.Name;
                }
                resultDto.ProductUnitId = item.ProductUnitId;
                resultDto.ProductUnitName = item.ProductUnit.Name;
                resultDto.Name = item.Name;
                resultDto.Spec = item.Spec;
                resultDto.SourceType = item.SourceType;
                resultDto.Price = null;

                var now = Clock.Now;
                var purchasePrice = queryable.Where(m => m.CustomerId == input.CustomerId && m.ValidDate >= now).OrderByDescending(m => m.ValidDate).FirstOrDefault();
                if (purchasePrice != null)
                {
                    var purchasePriceDetail = purchasePrice.Details.Where(m => m.ProductId == item.Id).FirstOrDefault();
                    if (purchasePriceDetail != null)
                    {
                        resultDto.Price = purchasePriceDetail.Price;
                        resultDto.TaxRate = purchasePriceDetail.TaxRate;
                    }
                }
                resultDtoList.Add(resultDto);
            }

            return new PagedResultDto<ProductWithPriceDto>(totalCount, resultDtoList);
        }

    }
}
