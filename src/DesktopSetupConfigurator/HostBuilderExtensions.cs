using DesktopSetupConfigurator.Core.Services;
using DesktopSetupConfigurator.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesktopSetupConfigurator;

public static class HostBuilderExtensions
{
    public static void ConfigureHost(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((services) =>
        {
            services.ConfigureServices();
        });
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContextFactory<CommandsDbContext>(options
            => options.UseSqlite("DataSource=commands.db"));
        services.AddSingleton<IDataService, DataService>();
    }
}
