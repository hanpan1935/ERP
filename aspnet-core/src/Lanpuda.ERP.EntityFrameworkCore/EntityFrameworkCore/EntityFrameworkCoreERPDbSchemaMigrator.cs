using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.Data;
using Volo.Abp.DependencyInjection;

namespace Lanpuda.ERP.EntityFrameworkCore;

public class EntityFrameworkCoreERPDbSchemaMigrator
    : IERPDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreERPDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the ERPDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ERPDbContext>()
            .Database
            .MigrateAsync();
    }
}
