using System;

namespace Lanpuda.ERP.SalesManagement.Customers.Dtos;

[Serializable]
public class CustomerUpdateDto
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
}