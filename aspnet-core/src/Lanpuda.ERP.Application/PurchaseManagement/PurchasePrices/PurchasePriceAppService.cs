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
using Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;
using System.Text.RegularExpressions;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices;


[Authorize]
public class PurchasePriceAppService : ERPAppService, IPurchasePriceAppService
{
    private readonly IPurchasePriceRepository _repository;
    private readonly IPurchasePriceDetailRepository _detailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;

    public PurchasePriceAppService(IPurchasePriceRepository repository, IPurchasePriceDetailRepository detailRepository, IUniqueCodeUtils uniqueCodeUtils)
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
    }


    [Authorize(ERPPermissions.PurchasePrice.Create)]
    public async Task CreateAsync(PurchasePriceCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchasePricePrefix); ;
        PurchasePrice purchasePrice = new PurchasePrice(id);
        purchasePrice.Number = number;
        purchasePrice.SupplierId = input.SupplierId;
        purchasePrice.AvgDeliveryDate = input.AvgDeliveryDate;
        purchasePrice.QuotationDate = input.QuotationDate;
        purchasePrice.Remark = input.Remark;


        List<PurchasePriceDetail> details = new List<PurchasePriceDetail>();
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            PurchasePriceDetail detail = new PurchasePriceDetail(detailId);
            detail.PurchasePriceId = id;
            detail.ProductId = item.ProductId;
            detail.Price = item.Price;
            detail.TaxRate = item.TaxRate;
            detail.Sort = i;
            details.Add(detail);
        }
      

        CheckIsProductRepeat(details);

        PurchasePrice result = await _repository.InsertAsync(purchasePrice);
        await _detailRepository.InsertManyAsync(details);
    }



    [Authorize(ERPPermissions.PurchasePrice.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        PurchasePrice purchasePrice = await _repository.FindAsync(id);
        if (purchasePrice == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _repository.DeleteAsync(purchasePrice);
    }



    [Authorize(ERPPermissions.PurchasePrice.Update)]
    public async Task<PurchasePriceDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        result.Details = result.Details.OrderBy(m=>m.Sort).ToList();
        return ObjectMapper.Map<PurchasePrice, PurchasePriceDto>(result);
    }



    [Authorize(ERPPermissions.PurchasePrice.Default)]
    public async Task<PagedResultDto<PurchasePriceDto>> GetPagedListAsync(PurchasePricePagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.SupplierId != null, m=>m.SupplierId == input.SupplierId)
            .WhereIf(input.QuotationDateStart != null , m=>m.QuotationDate >=input.QuotationDateStart)
            .WhereIf(input.QuotationDateEnd != null, m => m.QuotationDate <= input.QuotationDateStart)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<PurchasePriceDto>(totalCount, ObjectMapper.Map<List<PurchasePrice>, List<PurchasePriceDto>>(result));
    }



    [Authorize(ERPPermissions.PurchasePrice.Update)]
    public async Task UpdateAsync(Guid id, PurchasePriceUpdateDto input)
    {
        PurchasePrice purchasePrice = await _repository.FindAsync(id);
        if (purchasePrice == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //
      
       
        purchasePrice.SupplierId = input.SupplierId;
        purchasePrice.AvgDeliveryDate = input.AvgDeliveryDate;
        purchasePrice.QuotationDate = input.QuotationDate;
        purchasePrice.Remark = input.Remark;

        List<PurchasePriceDetail> createList = new List<PurchasePriceDetail>();
        List<PurchasePriceDetail> updateList = new List<PurchasePriceDetail>();
        List<PurchasePriceDetail> deleteList = new List<PurchasePriceDetail>();
        List<PurchasePriceDetail> dbList = await _detailRepository.GetListAsync(m => m.PurchasePriceId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                PurchasePriceDetail detail = new PurchasePriceDetail(detailId);
                detail.PurchasePriceId = id;
                detail.ProductId = item.ProductId;
                detail.Price = item.Price;
                detail.TaxRate = item.TaxRate;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                PurchasePriceDetail detail = dbList.Where(m => m.Id == item.Id).First();
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
        List<PurchasePriceDetail> details = new List<PurchasePriceDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);



        var result = await _repository.UpdateAsync(purchasePrice);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
    }

    //TODO 判断是否重复
    private void CheckIsProductRepeat(List<PurchasePriceDetail> details)
    {
        var groups = from m in details group m by m.ProductId ;

        foreach (var group in groups)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";

                foreach (var item in group)
                {

                    string errRow = "第" + (item.Sort +1 ).ToString() + "行,";
                    rowNumbers += errRow;
                }

                throw new UserFriendlyException("" + (rowNumbers) + "产品重复");
            }
        }
    }
}
