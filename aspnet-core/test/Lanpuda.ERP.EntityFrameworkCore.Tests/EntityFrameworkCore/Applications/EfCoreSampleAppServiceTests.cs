using Lanpuda.ERP.Samples;
using Xunit;

namespace Lanpuda.ERP.EntityFrameworkCore.Applications;

[Collection(ERPTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ERPEntityFrameworkCoreTestModule>
{

}
