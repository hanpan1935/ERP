using Volo.Abp.Modularity;

namespace Lanpuda.ERP;

[DependsOn(
    typeof(ERPDomainModule),
    typeof(ERPTestBaseModule)
)]
public class ERPDomainTestModule : AbpModule
{

}
