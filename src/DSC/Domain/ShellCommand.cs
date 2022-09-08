using System.Text.Json.Serialization;

namespace DSC.Domain;

public record ShellCommand(string FileName, params string[] Arguments)
{
    [JsonIgnore]
    public string FileName { get; set; } = FileName;

    [JsonIgnore]
    public string[] Arguments { get; set; } = Arguments;

    [JsonPropertyName("profiles")]
    public IEnumerable<string> InstallProfiles { get; set; } = Enumerable.Empty<string>();

    [JsonPropertyName("command")]
    public string Command => FileName + " " + string.Join(" ", Arguments);
}