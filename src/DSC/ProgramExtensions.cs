using System.Text.Json;

using Microsoft.Extensions.Configuration;

namespace DSC;

public static class ProgramExtensions
{
    private static JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

    public static void BuildConfig(this IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");
    }

    public static string AsJson(this object entity)
    {
        return JsonSerializer.Serialize(entity, options);
    }
}
