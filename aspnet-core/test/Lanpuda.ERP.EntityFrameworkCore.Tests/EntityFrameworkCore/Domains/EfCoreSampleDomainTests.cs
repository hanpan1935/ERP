using Lanpuda.ERP.Samples;
using Xunit;

namespace Lanpuda.ERP.EntityFrameworkCore.Domains;

[Collection(ERPTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ERPEntityFrameworkCoreTestModule>
{

}
