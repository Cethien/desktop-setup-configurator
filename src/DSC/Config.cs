using System.Text.Json.Serialization;

public record Config
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = "<no username set>";

    [JsonPropertyName("profiles")]
    public IEnumerable<string> InstallProfiles { get; set; } = Enumerable.Empty<string>();

    [JsonPropertyName("defaultProfile")]
    public string? DefaultInstallProfile { get; set; }

    [JsonPropertyName("packageManagers")]
    public IEnumerable<string>? PackageManagers { get; set; } = Enumerable.Empty<string>();

    [JsonPropertyName("installCommands")]
    public IEnumerable<string>? InstallCommands { get; set; } = Enumerable.Empty<string>();

    [JsonPropertyName("postInstallCommands")]
    public IEnumerable<string>? PostInstallCommands { get; set; } = Enumerable.Empty<string>();
}
