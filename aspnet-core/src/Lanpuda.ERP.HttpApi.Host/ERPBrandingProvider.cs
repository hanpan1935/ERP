using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Lanpuda.ERP;

[Dependency(ReplaceServices = true)]
public class ERPBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ERP";
}
