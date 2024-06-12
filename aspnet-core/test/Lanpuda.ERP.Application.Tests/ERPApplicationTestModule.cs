using Volo.Abp.Modularity;

namespace Lanpuda.ERP;

[DependsOn(
    typeof(ERPApplicationModule),
    typeof(ERPDomainTestModule)
)]
public class ERPApplicationTestModule : AbpModule
{

}
