using System.Text.Json.Serialization;
using DSC.Domain;

public record Config
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = "<no username set>";

    [JsonPropertyName("profiles")]
    public IEnumerable<string> InstallProfiles { get; set; } = Enumerable.Empty<string>();

    [JsonPropertyName("defaultProfile")]
    public string? DefaultInstallProfile { get; set; }

    [JsonIgnore]
    public IEnumerable<PackageManager> PackageManagers { get; set; } = Enumerable.Empty<PackageManager>();

    [JsonPropertyName("installCommands")]
    public IEnumerable<ShellCommand>? InstallCommands { get; set; } = Enumerable.Empty<ShellCommand>();

    [JsonPropertyName("postInstallCommands")]
    public IEnumerable<ShellCommand>? PostInstallCommands { get; set; } = Enumerable.Empty<ShellCommand>();
}
