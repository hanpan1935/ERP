using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Lanpuda.ERP.Utils.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers;

[Authorize]
public class SupplierAppService : ERPAppService, ISupplierAppService
{
    private readonly ISupplierRepository _repository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;

    public SupplierAppService(ISupplierRepository repository, IUniqueCodeUtils uniqueCodeUtils)
    {
        _repository = repository;
        _uniqueCodeUtils = uniqueCodeUtils;
    }


    [Authorize(ERPPermissions.Supplier.Create)]
    public async Task CreateAsync(SupplierCreateDto input)
    {
        //TODO ȫ�Ʋ����ظ�
        bool hasSameFullName = await _repository.AnyAsync(m => m.FullName == input.FullName);
        if (hasSameFullName == true)
        {
            throw new UserFriendlyException("�Ѿ�������ͬ�Ĺ�Ӧ��ȫ��:" + input.FullName);
        }


        //TODO ��Ʋ����ظ�
        bool hasSameShortName = await _repository.AnyAsync(m => m.ShortName == input.ShortName);
        if (hasSameShortName)
        {
            throw new UserFriendlyException("�Ѿ�������ͬ�Ĺ�Ӧ�̼��:" + input.ShortName);
        }


        Guid id = GuidGenerator.Create();
        Supplier supplier = new Supplier(id);
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync("SU");
        supplier.Number = number;
        supplier.FullName = input.FullName;
        supplier.ShortName = input.ShortName;
        supplier.FactoryAddress = input.FactoryAddress;
        supplier.Contact = input.Contact;
        supplier.ContactTel = input.ContactTel;
        supplier.OrganizationName = input.OrganizationName;
        supplier.TaxNumber = input.TaxNumber;
        supplier.BankName = input.BankName;
        supplier.AccountNumber = input.AccountNumber;
        supplier.TaxAddress = input.TaxAddress;
        supplier.TaxTel = input.TaxTel;
        Supplier result = await _repository.InsertAsync(supplier);
    }


    [Authorize(ERPPermissions.Supplier.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        //TODO �ж��ܷ�ɾ�� 1: ���۱���  2 ���۶���
        Supplier supplier = await _repository.FindAsync(id);
        if (supplier == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _repository.DeleteAsync(supplier);
    }



    [Authorize(ERPPermissions.Supplier.Update)]
    public async Task<SupplierDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        var dto = ObjectMapper.Map<Supplier, SupplierDto>(result);
        return dto;
    }


    [Authorize(ERPPermissions.Supplier.Default)]
    public async Task<PagedResultDto<SupplierDto>> GetPagedListAsync(SupplierPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ShortName), m => m.ShortName.Contains(input.ShortName))
            .WhereIf(!string.IsNullOrEmpty(input.FullName), m => m.FullName.Contains(input.FullName))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SupplierDto>(totalCount, ObjectMapper.Map<List<Supplier>, List<SupplierDto>>(result));
    }



    [Authorize(ERPPermissions.Supplier.Update)]
    public async Task UpdateAsync(Guid id, SupplierUpdateDto input)
    {
        //TODO ȫ�Ʋ����ظ�

        //TODO ��Ʋ����ظ�
        Supplier supplier = await _repository.FindAsync(id);
        if (supplier == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        supplier.FullName = input.FullName;
        supplier.ShortName = input.ShortName;
        supplier.FactoryAddress = input.FactoryAddress;
        supplier.Contact = input.Contact;
        supplier.ContactTel = input.ContactTel;
        supplier.OrganizationName = input.OrganizationName;
        supplier.TaxNumber = input.TaxNumber;
        supplier.BankName = input.BankName;
        supplier.AccountNumber = input.AccountNumber;
        supplier.TaxAddress = input.TaxAddress;
        supplier.TaxTel = input.TaxTel;
        var result = await _repository.UpdateAsync(supplier);
    }



    public async Task<List<SupplierDto>> GetAllAsync()
    {
        var result = await _repository.GetListAsync();
        result = result.OrderByDescending(m=>m.CreationTime).ToList();
        return ObjectMapper.Map<List<Supplier>, List<SupplierDto>>(result);
    }
}
