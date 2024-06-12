using Volo.Abp.Modularity;

namespace Lanpuda.ERP;

public abstract class ERPApplicationTestBase<TStartupModule> : ERPTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
