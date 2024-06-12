using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.SalesManagement.Customers;

[Authorize]
public class CustomerAppService : ERPAppService, ICustomerAppService
{
    private readonly ICustomerRepository _repository;

    public CustomerAppService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    [Authorize(ERPPermissions.Customer.Create)]
    public async Task CreateAsync(CustomerCreateDto input)
    {
        //TODO 全称不能重复

        //TODO 简称不能重复

        Guid id = GuidGenerator.Create();
        Customer customer = new Customer(id);
        customer.Number = input.Number;
        customer.FullName = input.FullName;
        customer.ShortName = input.ShortName;
        customer.Contact = input.Contact;
        customer.ContactTel = input.ContactTel;
        customer.ShippingAddress = input.ShippingAddress;
        customer.Consignee = input.Consignee;
        customer.ConsigneeTel = input.ConsigneeTel;
        customer.OrganizationName = input.OrganizationName;
        customer.TaxNumber = input.TaxNumber;
        customer.BankName = input.BankName;
        customer.AccountNumber = input.AccountNumber;
        customer.TaxAddress = input.TaxAddress;
        customer.TaxTel = input.TaxTel;
        customer.Description = input.Description;
        Customer result = await _repository.InsertAsync(customer);
    }



    [Authorize(ERPPermissions.Customer.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        //TODO 判断能否删除 1: 销售报价  2 销售订单

        Customer customer = await _repository.FindAsync(id);
        if (customer == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _repository.DeleteAsync(customer);
    }

    [Authorize(ERPPermissions.Customer.Update)]
    public async Task<CustomerDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<Customer, CustomerDto>(result);
    }

    [Authorize(ERPPermissions.Customer.Default)]
    public async Task<PagedResultDto<CustomerDto>> GetPagedListAsync(CustomerPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.FullName), m => m.FullName.Contains(input.FullName))
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<CustomerDto>(totalCount, ObjectMapper.Map<List<Customer>, List<CustomerDto>>(result));
    }

    [Authorize(ERPPermissions.Customer.Update)]
    public async Task UpdateAsync(Guid id, CustomerUpdateDto input)
    {

        //TODO 全称不能重复

        //TODO 简称不能重复


        Customer customer = await _repository.FindAsync(id);
        if (customer == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        customer.Number = input.Number;
        customer.FullName = input.FullName;
        customer.ShortName = input.ShortName;
        customer.Contact = input.Contact;
        customer.ContactTel = input.ContactTel;
        customer.ShippingAddress = input.ShippingAddress;
        customer.Consignee = input.Consignee;
        customer.ConsigneeTel = input.ConsigneeTel;
        customer.OrganizationName = input.OrganizationName;
        customer.TaxNumber = input.TaxNumber;
        customer.BankName = input.BankName;
        customer.AccountNumber = input.AccountNumber;
        customer.TaxAddress = input.TaxAddress;
        customer.TaxTel = input.TaxTel;
        customer.Description = input.Description;

        var result = await _repository.UpdateAsync(customer);
    }

    [Authorize]
    public async Task<List<CustomerLookupDto>> GetAllAsync()
    {
        var result = await _repository.GetListAsync();
        result = result.OrderByDescending(m => m.CreationTime).ToList();
        return ObjectMapper.Map<List<Customer>, List<CustomerLookupDto>>(result);
    }
}
