using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;

[Serializable]
public class PurchasePriceDetailDto : AuditedEntityDto<Guid>
{
    public Guid PurchasePriceId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public double Price { get; set; }

    public double TaxRate { get; set; }



    public string NetPrice
    {
        get
        {
            //double price = 0;
            //bool isPrice = double.TryParse(this.Price, out price);
            //double taxRate = 0;
            //bool isTaxRate = double.TryParse(TaxRate, out taxRate);

            //if (isPrice != true || isTaxRate != true)
            //{
            //    return "";
            //}
            double result = Price - (Price * (TaxRate / 100));
            return result.ToString();
        }
        

    }
}