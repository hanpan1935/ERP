using System;

namespace Lanpuda.ERP.WarehouseManagement.Locations.Dtos;

[Serializable]
public class LocationUpdateDto
{
    public Guid WarehouseId { get; set; }


    public string Number { get; set; }

    public string Name { get; set; }

    public string Remark { get; set; }
}