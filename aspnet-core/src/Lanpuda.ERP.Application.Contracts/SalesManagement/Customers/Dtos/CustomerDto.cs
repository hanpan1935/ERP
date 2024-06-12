using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.Customers.Dtos;

[Serializable]
public class CustomerDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public string FullName { get; set; }

    public string ShortName { get; set; }

    public string Contact { get; set; }

    public string ContactTel { get; set; }

    public string ShippingAddress { get; set; }

    public string Consignee { get; set; }

    public string ConsigneeTel { get; set; }

    public string OrganizationName { get; set; }

    public string TaxNumber { get; set; }

    public string BankName { get; set; }

    public string AccountNumber { get; set; }

    public string TaxAddress { get; set; }

    public string TaxTel { get; set; }

    public string Description { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}