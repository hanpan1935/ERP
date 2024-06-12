using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using Lanpuda.ERP.Utils.UniqueCode;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Lanpuda.ERP.ProductionManagement.Boms;

[Authorize]
public class BomAppService : ERPAppService, IBomAppService
{
    private readonly IBomRepository _bomRepository;
    private readonly IBomDetailRepository _bomDetailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;

    public BomAppService(IBomRepository repository, IBomDetailRepository detailRepository, IUniqueCodeUtils uniqueCodeUtils)
    {
        _bomRepository = repository;
        _bomDetailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
    }


    [Authorize(ERPPermissions.Bom.Create)]
    public async Task CreateAsync(BomCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        Bom bom = new Bom(id);
        bom.ProductId = input.ProductId;
        bom.Remark = input.Remark;


        if (input.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        //一对1关系,如果已经存在,无法新建
        var  hasBom = await _bomRepository.FindAsync(id);
        if (hasBom != null) { throw new UserFriendlyException("已经存在,无法再次新建"); }


        List<BomDetail> details = new List<BomDetail>();

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            BomDetail detail = new BomDetail(detailId);
            detail.BomId = id;
            detail.ProductId = item.ProductId;
            detail.Quantity = item.Quantity;
            detail.Remark = item.Remark;
            detail.Sort = i;
            details.Add(detail);
        }


        CheckIsProductRepeat(details);

        Bom result = await _bomRepository.InsertAsync(bom);
        await _bomDetailRepository.InsertManyAsync(details);
    }


    [Authorize(ERPPermissions.Bom.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Bom bom = await _bomRepository.FindAsync(id);
        if (bom == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        await _bomRepository.DeleteAsync(bom);
    }


    [Authorize(ERPPermissions.Bom.Update)]
    public async Task<BomDto> GetAsync(Guid id)
    {
        var result = await _bomRepository.FindAsync(id, true);
        return ObjectMapper.Map<Bom, BomDto>(result);
    }



    [Authorize(ERPPermissions.Bom.Default)]
    public async Task<PagedResultDto<BomDto>> GetPagedListAsync(BomPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _bomRepository.WithDetailsAsync();
        query = query
            .WhereIf(input.ProductId != null, m => m.ProductId.Equals(input.ProductId))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.Product.Name.Equals(input.ProductName))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<BomDto>(totalCount, ObjectMapper.Map<List<Bom>, List<BomDto>>(result));
    }


    [Authorize(ERPPermissions.Bom.Update)]
    public async Task UpdateAsync(Guid id, BomUpdateDto input)
    {
        Bom bom = await _bomRepository.FindAsync(id);
        if (bom == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (input.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        bom.ProductId = input.ProductId;
        bom.Remark = input.Remark;

        List<BomDetail> createList = new List<BomDetail>();
        List<BomDetail> updateList = new List<BomDetail>();
        List<BomDetail> deleteList = new List<BomDetail>();
        List<BomDetail> dbList = await _bomDetailRepository.GetListAsync(m => m.BomId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                BomDetail detail = new BomDetail(detailId);
                detail.BomId = id;
                detail.ProductId = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.Remark = item.Remark;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                BomDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.Remark = item.Remark;
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
        List<BomDetail> details = new List<BomDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);

        var result = await _bomRepository.UpdateAsync(bom);
        await _bomDetailRepository.InsertManyAsync(createList);
        await _bomDetailRepository.UpdateManyAsync(updateList);
        await _bomDetailRepository.DeleteManyAsync(deleteList);
    }



    //TODO 判断是否重复
    private void CheckIsProductRepeat(List<BomDetail> details)
    {
        var res = from m in details group m by m.ProductId;

        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += item.Sort + 1 + ",";
                }
                throw new UserFriendlyException("第:" + rowNumbers + "行产品重复");
            }
        }
    }



    ///// <summary>
    ///// 根据产品Id和数量获得Bom明细，不递归
    ///// </summary>
    ///// <param name = "input" ></ param >
    ///// < returns ></ returns >
    ////public async Task<List<BomLookupDto>> GetForWorkOrderAsync(BomLookupRequestDto input)
    ////{
    ////    var queryable = await _bomRepository.WithDetailsAsync();
    ////    queryable = queryable.Where(m => m.ProductId == input.ProductId && m.IsActive == true);
    ////    var result = await AsyncExecuter.FirstOrDefaultAsync(queryable);


    ////    List<BomLookupDto> bomLookupDtos = new List<BomLookupDto>();
    ////    if (result == null)
    ////    {
    ////        return bomLookupDtos;
    ////    }
    ////    foreach (var item in result.Details)
    ////    {
    ////        BomLookupDto dto = ObjectMapper.Map<BomDetail, BomLookupDto>(item);
    ////        dto.Quantity = item.Quantity * input.Quantity;
    ////        bomLookupDtos.Add(dto);
    ////    }

    ////    return bomLookupDtos;
    ////}


    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    //public async Task<List<BomTreeDto>> GetBomTreeAsync(BomLookupRequestDto input)
    //{
    //    List<BomTreeDto> details = new List<BomTreeDto>();

    //    var query = await _bomRepository.WithDetailsAsync();
    //    query = query.Where(m => m.ProductId == input.ProductId == true);
    //    var rootBom = await AsyncExecuter.FirstOrDefaultAsync(query);
    //    if (rootBom == null) return null;

    //    BomTreeDto rootTree = new BomTreeDto();
    //    rootTree.Id = Guid.Empty;
    //    rootTree.ParentId = Guid.Empty;
    //    rootTree.ProductName = rootBom.Product.Name;
    //    rootTree.ProductId = rootBom.ProductId;
    //    rootTree.ProductSourceType = rootBom.Product.SourceType;
    //    rootTree.Quantity = 1;
    //    details.Add(rootTree);


    //    await GetDetailsAsync(details, rootTree, rootTree.Id);



    //    return details;
    //}

    private async Task GetDetailsAsync(List<BomTreeDto> resultDetails, BomTreeDto parentTreeNode, Guid parentId)
    {
        var query = await _bomRepository.WithDetailsAsync();
        query = query.Where(m => m.ProductId == parentTreeNode.ProductId);
        var bom = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (bom == null) return;
        foreach (BomDetail bomDetail in bom.Details)
        {
            BomTreeDto bomTree = new BomTreeDto();
            bomTree.Id = bomDetail.Id;
            bomTree.ParentId = parentId;
            bomTree.ProductId = bomDetail.ProductId;
            bomTree.ProductNumber = bomDetail.Product.Number;
            bomTree.ProductName = bomDetail.Product.Name;
            bomTree.ProductSpec = bomDetail.Product.Spec;
            bomTree.ProductUnitName = bomDetail.Product.ProductUnit.Name;
            bomTree.ProductSourceType = bomDetail.Product.SourceType;
            bomTree.Quantity = bomDetail.Quantity;
            resultDetails.Add(bomTree);
            await GetDetailsAsync(resultDetails, bomTree, bomTree.Id);
        }
    }
}
