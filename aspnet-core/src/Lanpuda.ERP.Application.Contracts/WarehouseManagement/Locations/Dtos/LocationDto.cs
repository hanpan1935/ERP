using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Locations.Dtos;

[Serializable]
public class LocationDto : EntityDto<Guid>
{
    public Guid WarehouseId { get; set; }

    public string WarehouseName { get; set; }

    public string Number { get; set; }

    public string Name { get; set; }

    public string Remark { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}