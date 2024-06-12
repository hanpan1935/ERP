using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;

[Serializable]
public class PurchaseStorageUpdateDto
{
    public string Remark { get; set; }

    public List<PurchaseStorageDetailUpdateDto> Details { get; set; }


    public PurchaseStorageUpdateDto()
    {
        Details = new List<PurchaseStorageDetailUpdateDto>();
    }
}