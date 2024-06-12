using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;

[Serializable]
public class ProcessInspectionPagedRequestDto : PagedAndSortedResultRequestDto
{
    public string? Number { get; set; }

    public string? WorkOrderNumber { get; set; }

    public string? ProductName { get; set; }
}