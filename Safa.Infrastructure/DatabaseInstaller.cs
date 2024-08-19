using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Safa.Infrastructure;

public static class DatabaseInstaller
{
    public static IServiceCollection InstallDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        
        // TODO: Add connection strings when database is in use
        services.AddPooledDbContextFactory<SafaDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString(""), ConfigureSqlOptions)
                .UseLazyLoadingProxies();
        });

        return services.AddDbContextPool<SafaDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString(""), ConfigureSqlOptions)
                .UseLazyLoadingProxies();
        });
    }

    private static void ConfigureSqlOptions(SqlServerDbContextOptionsBuilder builder) =>
        builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
}