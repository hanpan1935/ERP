using Xunit;

namespace Lanpuda.ERP.EntityFrameworkCore;

[CollectionDefinition(ERPTestConsts.CollectionDefinitionName)]
public class ERPEntityFrameworkCoreCollection : ICollectionFixture<ERPEntityFrameworkCoreFixture>
{

}
