using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;

[Serializable]
public class FinalInspectionPagedRequestDto : PagedAndSortedResultRequestDto
{
    public string? Number { get; set; }

    public string? MpsNumber { get; set; }

    public string? ProductName { get; set; }
}