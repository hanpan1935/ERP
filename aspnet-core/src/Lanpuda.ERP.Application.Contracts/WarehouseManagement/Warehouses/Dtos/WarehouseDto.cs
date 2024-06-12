using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;

[Serializable]
public class WarehouseDto : EntityDto<Guid>
{
    public string Number { get; set; }

    public string Name { get; set; }

    public string Remark { get; set; }

    public List<LocationDto> Locations { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }


    public WarehouseDto()
    {
        this.Locations = new List<LocationDto>();
    }
}