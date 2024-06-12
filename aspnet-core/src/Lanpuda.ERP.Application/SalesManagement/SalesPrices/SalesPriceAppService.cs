using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;
using Lanpuda.ERP.Utils.UniqueCode;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.SalesManagement.SalesPrices;

[Authorize]
public class SalesPriceAppService :ERPAppService,ISalesPriceAppService
{

    private readonly ISalesPriceRepository _repository;
    private readonly ISalesPriceDetailRepository _detailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;


    public SalesPriceAppService(ISalesPriceRepository repository, ISalesPriceDetailRepository detailRepository, IUniqueCodeUtils uniqueCodeUtils) 
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
    }


    [Authorize(ERPPermissions.SalesPrice.Create)]
    public async Task CreateAsync(SalesPriceCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.SalesPricePrefix); ;
        SalesPrice salesPrice = new SalesPrice(id);
        salesPrice.Number = number;
        salesPrice.CustomerId = input.CustomerId;
        salesPrice.ValidDate = input.ValidDate;
        salesPrice.Remark = input.Remark;


        List<SalesPriceDetail> details = new List<SalesPriceDetail>();
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            SalesPriceDetail detail = new SalesPriceDetail(detailId);
            detail.SalesPriceId = id;
            detail.ProductId = item.ProductId;
            detail.Price = item.Price;
            detail.TaxRate = item.TaxRate;
            detail.Sort= item.Sort;
            details.Add(detail);
        }

        CheckIsProductRepeat(details);

        SalesPrice result = await _repository.InsertAsync(salesPrice);
        await _detailRepository.InsertManyAsync(details);
    }

    [Authorize(ERPPermissions.SalesPrice.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        SalesPrice salesPrice = await _repository.FindAsync(id);
        if (salesPrice == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
       
        await _repository.DeleteAsync(salesPrice);
    }


    [Authorize(ERPPermissions.SalesPrice.Update)]
    public async Task<SalesPriceDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        result.Details = result.Details.OrderBy(m=>m.Sort).ToList();
        return ObjectMapper.Map<SalesPrice, SalesPriceDto>(result);
    }

    [Authorize(ERPPermissions.SalesPrice.Default)]
    public async Task<PagedResultDto<SalesPriceDto>> GetPagedListAsync(SalesPricePagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.CustomerName), m => m.Customer.FullName.Contains(input.CustomerName))
            .WhereIf(input.ValidDate != null, m => m.ValidDate <= ((DateTime)input.ValidDate).Date)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SalesPriceDto>(totalCount, ObjectMapper.Map<List<SalesPrice>, List<SalesPriceDto>>(result));
    }

    [Authorize(ERPPermissions.SalesPrice.Update)]
    public async Task UpdateAsync(Guid id, SalesPriceUpdateDto input)
    {
        SalesPrice salesPrice = await _repository.FindAsync(id);
        if (salesPrice == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }


        salesPrice.CustomerId = input.CustomerId;
        salesPrice.ValidDate = input.ValidDate;
        salesPrice.Remark = input.Remark;

        List<SalesPriceDetail> createList = new List<SalesPriceDetail>();
        List<SalesPriceDetail> updateList = new List<SalesPriceDetail>();
        List<SalesPriceDetail> deleteList = new List<SalesPriceDetail>();
        List<SalesPriceDetail> dbList = await _detailRepository.GetListAsync(m => m.SalesPriceId == id);

        

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];    
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                SalesPriceDetail detail = new SalesPriceDetail(detailId);
                detail.SalesPriceId = id;
                detail.ProductId = item.ProductId;
                detail.Price = item.Price;
                detail.TaxRate = item.TaxRate;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                SalesPriceDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.SalesPriceId = id;
                detail.ProductId = item.ProductId;
                detail.Price = item.Price;
                detail.TaxRate = item.TaxRate;
                detail.Sort = i;
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
        List<SalesPriceDetail> details = new List<SalesPriceDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);

        var result = await _repository.UpdateAsync(salesPrice);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }


    //TODO 判断是否重复
    private void CheckIsProductRepeat(List<SalesPriceDetail> details)
    {
        var res = from m in details group m by m.ProductId;

        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += (item.Sort +1 ).ToString() + "行,";
                }
                throw new UserFriendlyException("第:" + rowNumbers + "产品重复");
            }
        }
    }
}
