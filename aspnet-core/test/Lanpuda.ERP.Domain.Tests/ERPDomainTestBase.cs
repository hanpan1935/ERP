using Volo.Abp.Modularity;

namespace Lanpuda.ERP;

/* Inherit from this class for your domain layer tests. */
public abstract class ERPDomainTestBase<TStartupModule> : ERPTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
