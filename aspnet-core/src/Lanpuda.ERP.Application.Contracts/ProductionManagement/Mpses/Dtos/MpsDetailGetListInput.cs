using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos;

[Serializable]
public class MpsDetailGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? MpsId { get; set; }

    public DateTime? ProductionDate { get; set; }

    public double? Quantity { get; set; }

    public string Remark { get; set; }
}