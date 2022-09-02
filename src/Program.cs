using System.Text.Json;
using System.Text.Json.Serialization;


//create sample
var userName = "cethien";
var installProfiles = new[]
    {
        "any",
        "private",
        "work",
    };
var defaultProfile = installProfiles.ToList().First(x => x == "any");
var pacMans = new[]
{
    new PackageManager("winget", "winget.exe")
};
var installCommands = new[]
{
    new ShellCommand(pacMans.First(x => x.Name == "winget").FileName,
                    new[]{
                        "install",
                        "-e",
                        "--id Microsoft.Terminal",
                        "-s winget"
                        }
                    ){
                        InstallProfiles = installProfiles.Where(x => x == "any")
                    },

    new ShellCommand(pacMans.First(x => x.Name == "winget").FileName,
                    new[]{
                        "install",
                        "-e",
                        "--id Microsoft.PowerShell",
                        "-s winget"
                        }
                    ){
                        InstallProfiles = installProfiles.Where(x => x == "any")
                    },

    new ShellCommand(pacMans.First(x => x.Name == "winget").FileName,
                    new[]{
                        "install",
                        "-e",
                        "--id Git.Git",
                        "-s winget"
                        }
                    ){
                        InstallProfiles = installProfiles.Where(x => x == "any")
                    },

    new ShellCommand(pacMans.First(x => x.Name == "winget").FileName,
                    new[]{
                        "install",
                        "-e",
                        "--id JanDeDobbeleer.OhMyPosh",
                        "-s winget"
                        }
                    ){
                        InstallProfiles = installProfiles.Where(x => x == "any")
                    },
};
var postInstallCommands = new[]
{
    new ShellCommand("pwsh",
                    new[]{
                        "-Command {\"Clear-Host`noh-my-posh init pwsh --config `\"$env:POSH_THEMES_PATH\\clean-detailed.omp.json`\" | Invoke-Expression\" > $profile}"
                        }
                    ){
                        InstallProfiles = installProfiles.Where(x => x == "any")
                    }
};

var config = new Config()
{
    UserName = userName,
    InstallProfiles = installProfiles,
    DefaultInstallProfile = defaultProfile,
    PackageManagers = pacMans,
    InstallCommands = installCommands,
    PostInstallCommands = postInstallCommands
};

// generate json
var serializerOptions = new JsonSerializerOptions { WriteIndented = true };
var json = JsonSerializer.Serialize(config, serializerOptions);

// print data
File.WriteAllText("./config.json", json);
System.Console.WriteLine("Generated Config File");


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

public record PackageManager(string Name, string FileName)
{
    public string Name { get; set; } = Name;

    public string FileName { get; set; } = FileName;

    public string? InstallCommand { get; set; }
}

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