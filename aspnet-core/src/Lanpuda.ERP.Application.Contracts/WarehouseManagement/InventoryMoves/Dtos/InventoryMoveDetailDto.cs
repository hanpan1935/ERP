using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;

[Serializable]
public class InventoryMoveDetailDto : EntityDto<Guid>
{
    public Guid InventoryMoveId { get; set; }

    public Guid ProductId { get; set; }

    public Guid OutLocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }



    public int Sort { get; set; }

    ////
    ///

    public string ProductName { get; set; }
    public string ProductNumber { get; set; }
    public string ProductSpec { get; set; }
    public string ProductUnitName { get; set; }

    public string OutWarehouseName { get; set; }
    public string OutLocationName { get; set; }


    public Guid InWarehouseId { get; set; }
    public string InWarehouseName { get;set; }
    public Guid InLocationId { get; set; }
    public string InLocationName { get; set;}


}