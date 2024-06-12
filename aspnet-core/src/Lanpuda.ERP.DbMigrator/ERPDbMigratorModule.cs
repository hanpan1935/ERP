using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lanpuda.ERP.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ERPEntityFrameworkCoreModule),
    typeof(ERPApplicationContractsModule)
    )]
public class ERPDbMigratorModule : AbpModule
{
}
