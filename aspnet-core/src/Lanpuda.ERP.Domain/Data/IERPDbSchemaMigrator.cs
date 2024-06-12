using System.Threading.Tasks;

namespace Lanpuda.ERP.Data;

public interface IERPDbSchemaMigrator
{
    Task MigrateAsync();
}
