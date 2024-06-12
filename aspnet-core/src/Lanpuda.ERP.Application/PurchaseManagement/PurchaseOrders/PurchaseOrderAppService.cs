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
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Volo.Abp.ObjectMapping;
using static Lanpuda.ERP.Permissions.ERPPermissions;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;
using Volo.Abp.Domain.Repositories;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders;

[Authorize]
public class PurchaseOrderAppService : ERPAppService, IPurchaseOrderAppService
{
    private readonly IPurchaseOrderRepository _repository;
    private readonly IPurchaseOrderDetailRepository _detailRepository;
    private readonly IArrivalNoticeDetailRepository _noticeDetailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;


    public PurchaseOrderAppService(
        IPurchaseOrderRepository repository, 
        IPurchaseOrderDetailRepository detailRepository,
        IArrivalNoticeDetailRepository noticeDetailRepository,
        IUniqueCodeUtils uniqueCodeUtils)
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _noticeDetailRepository = noticeDetailRepository;
    }


    [Authorize(ERPPermissions.PurchaseOrder.Create)]
    public async Task CreateAsync(PurchaseOrderCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.PurchaseOrderPrefix); ;
        PurchaseOrder purchaseOrder = new PurchaseOrder(id);
        purchaseOrder.SupplierId = input.SupplierId;
        purchaseOrder.Number = number;
        purchaseOrder.RequiredDate = input.RequiredDate;
        purchaseOrder.PromisedDate = input.PromisedDate;
        purchaseOrder.OrderType = PurchaseOrderType.ManualOrder;
        purchaseOrder.Contact = input.Contact;
        purchaseOrder.ContactTel = input.ContactTel;
        purchaseOrder.ShippingAddress = input.ShippingAddress;
        purchaseOrder.Remark = input.Remark;
        purchaseOrder.CloseStatus = PurchaseOrderCloseStatus.Opened;
        purchaseOrder.IsConfirmed = false;
       

        List<PurchaseOrderDetail> details = new List<PurchaseOrderDetail>();

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item    = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            PurchaseOrderDetail detail = new PurchaseOrderDetail(detailId);
            detail.PurchaseOrderId = id;
            detail.ProductId = item.ProductId;
            detail.PromiseDate = item.PromiseDate;
            detail.Quantity = item.Quantity;
            detail.Price = item.Price;
            detail.TaxRate = item.TaxRate;
            detail.Remark = item.Remark;
            detail.Sort = i;
            details.Add(detail);
        }

        CheckIsProductRepeat(details);
        PurchaseOrder result = await _repository.InsertAsync(purchaseOrder);
        await _detailRepository.InsertManyAsync(details);
    }



    [Authorize(ERPPermissions.PurchaseOrder.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        PurchaseOrder purchaseOrder = await _repository.FindAsync(id);
        if (purchaseOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //����֪ͨ
        foreach (var item in purchaseOrder.Details)
        {
            var hasNotice = await _noticeDetailRepository.AnyAsync(m => m.PurchaseOrderDetailId == item.Id);
            if (hasNotice)
            {
                throw new UserFriendlyException("����ɾ����Ӧ������֪ͨ");
            }
        }
        await _repository.DeleteAsync(purchaseOrder);
    }



    [Authorize(ERPPermissions.PurchaseOrder.Update)]
    public async Task<PurchaseOrderDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        return ObjectMapper.Map<PurchaseOrder, PurchaseOrderDto>(result);
    }



    [Authorize(ERPPermissions.PurchaseOrder.Default)]
    public async Task<PagedResultDto<PurchaseOrderDto>> GetPagedListAsync(PurchaseOrderPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(input.SupplierId != null, m => m.SupplierId == input.SupplierId)
            .WhereIf(input.RequiredDateStart != null, m=>m.RequiredDate >= input.RequiredDateStart)
            .WhereIf(input.RequiredDateEnd != null , m=>m.RequiredDate <= input.RequiredDateEnd)
            .WhereIf(input.OrderType != null, m => m.OrderType == input.OrderType)
            .WhereIf(input.CloseStatus != null, m => m.CloseStatus == input.CloseStatus)
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed == input.IsConfirmed)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<PurchaseOrderDto>(totalCount, ObjectMapper.Map<List<PurchaseOrder>, List<PurchaseOrderDto>>(result));
    }



    [Authorize(ERPPermissions.PurchaseOrder.Update)]
    public async Task UpdateAsync(Guid id, PurchaseOrderUpdateDto input)
    {
        PurchaseOrder purchaseOrder = await _repository.FindAsync(id);
        if (purchaseOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //״̬��֤ ֻ��δȷ�ϵĲ��ܱ༭ IsConfirmed = false;
        if (purchaseOrder.IsConfirmed == true)
        {
            throw new UserFriendlyException("�Ѿ�ȷ���޷��༭");
        }
        //
        purchaseOrder.SupplierId = input.SupplierId;
        purchaseOrder.RequiredDate = input.RequiredDate;
        purchaseOrder.PromisedDate = input.PromisedDate;
        purchaseOrder.OrderType = PurchaseOrderType.ManualOrder;
        purchaseOrder.Contact = input.Contact;
        purchaseOrder.ContactTel = input.ContactTel;
        purchaseOrder.ShippingAddress = input.ShippingAddress;
        purchaseOrder.Remark = input.Remark;
        purchaseOrder.CloseStatus = PurchaseOrderCloseStatus.Opened;
        purchaseOrder.IsConfirmed = false;
        

        List<PurchaseOrderDetail> createList = new List<PurchaseOrderDetail>();
        List<PurchaseOrderDetail> updateList = new List<PurchaseOrderDetail>();
        List<PurchaseOrderDetail> deleteList = new List<PurchaseOrderDetail>();
        List<PurchaseOrderDetail> dbList = await _detailRepository.GetListAsync(m => m.PurchaseOrderId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //�½�
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                PurchaseOrderDetail detail = new PurchaseOrderDetail(detailId);
                detail.PurchaseOrderId = id;
                detail.ProductId = item.ProductId;
                detail.PromiseDate = item.PromiseDate;
                detail.Quantity = item.Quantity;
                detail.Price = item.Price;
                detail.TaxRate = item.TaxRate;
                detail.Remark = item.Remark;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //�༭
            {
                PurchaseOrderDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.PromiseDate = item.PromiseDate;
                detail.Quantity = item.Quantity;
                detail.Price = item.Price;
                detail.TaxRate = item.TaxRate;
                detail.Remark = item.Remark;
                detail.Sort = i;
                updateList.Add(detail);
            }
        }

        
        //ɾ��
        foreach (var dbItem in dbList)
        {
            bool isExsiting = input.Details.Any(m => m.Id == dbItem.Id);
            if (isExsiting == false)
            {
                deleteList.Add(dbItem);
            }
        }


        //����
        List<PurchaseOrderDetail> details = new List<PurchaseOrderDetail>();
        details.Union(createList);
        details.Union(updateList);
        CheckIsProductRepeat(details);

        var result = await _repository.UpdateAsync(purchaseOrder);
        await _detailRepository.InsertManyAsync(createList);
        await _detailRepository.UpdateManyAsync(updateList);
        await _detailRepository.DeleteManyAsync(deleteList);
		
    }



    [Authorize(ERPPermissions.PurchaseOrder.Close)]
    public async Task CloseAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);

        if (result == null)
        {
            throw new EntityNotFoundException("ϵͳû���ҵ��˲ɹ�����!�����Ѿ���ɾ��");
        }

        if (result.IsConfirmed == false)
        {
            throw new UserFriendlyException("δȷ�ϵĶ������ܹر�,����ֱ��ɾ��");
        }

        if (result.CloseStatus != PurchaseOrderCloseStatus.Opened)
        {
            throw new UserFriendlyException("�Ѿ��رյĶ���,�޷��ٴιر�");
        }
        result.CloseStatus = PurchaseOrderCloseStatus.ManualClosed;
        await _repository.UpdateAsync(result);
    }



    [Authorize(ERPPermissions.PurchaseOrder.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        var purchaseOrder = await _repository.FindAsync(id, true);

        if (purchaseOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (purchaseOrder.IsConfirmed == true)
        {
            throw new UserFriendlyException("�Ѿ�ȷ����");
        }

        if (purchaseOrder.Details == null || purchaseOrder.Details.Count == 0)
        {
            throw new UserFriendlyException("������ϸ����Ϊ��");
        }

        foreach (var item in purchaseOrder.Details)
        {
            if (item.Price < 0)
            {
                throw new UserFriendlyException("��Ʒ��" + item.Product.Name + ",�۸���Ϊ����");
            }

            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("��Ʒ��" + item.Product.Name + ",�����������0");
            }
        }

        purchaseOrder.IsConfirmed= true;
        purchaseOrder.ConfirmedTime = Clock.Now;
        purchaseOrder.ConfirmeUserId = CurrentUser.Id;
        await _repository.UpdateAsync(purchaseOrder);
    }


    [Authorize(ERPPermissions.ArrivalNotice.Create)]
    [Authorize(ERPPermissions.ArrivalNotice.Update)]
    public async Task<PagedResultDto<PurchaseOrderDetailSelectDto>> GetDetailPagedListAsync(PurchaseOrderDetailPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _detailRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.Product.Name.Contains(input.ProductName))
            .WhereIf(!string.IsNullOrEmpty(input.PurchaseOrderNumber), m => m.PurchaseOrder.Number.Contains(input.PurchaseOrderNumber))
            .WhereIf(!string.IsNullOrEmpty(input.SupplierName), m => m.PurchaseOrder.Supplier.FullName.Contains(input.SupplierName))
            .WhereIf(input.SupplierId != null, m => m.PurchaseOrder.SupplierId == input.SupplierId)
            .WhereIf(input.ProductId != null, m => m.ProductId >= input.ProductId)
            .WhereIf(input.IsConfirmed != null, m => m.PurchaseOrder.IsConfirmed == input.IsConfirmed)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<PurchaseOrderDetailSelectDto>(totalCount, ObjectMapper.Map<List<PurchaseOrderDetail>, List<PurchaseOrderDetailSelectDto>>(result));
    }



    private void CheckIsProductRepeat(List<PurchaseOrderDetail> details)
    {
        var res = from m in details group m by m.ProductId;

        foreach (var group in res)
        {
            if (group.Count() > 1)
            {
                string rowNumbers = "";
                foreach (var item in group)
                {
                    rowNumbers += (item.Sort + 1) + "";
                }
                throw new UserFriendlyException("��:" + rowNumbers + "��Ʒ�ظ�");
            }
        }
    }
}
