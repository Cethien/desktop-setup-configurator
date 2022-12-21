using DesktopSetupConfigurator.Database.Models.Common;

namespace DesktopSetupConfigurator.Models;

public class Tag : DbModel
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public bool? IsDefault { get; set; }
}

public sealed class InstallatonStage : Tag { }
public sealed class InstallationProfile : Tag { }
public sealed class InstallationEnvironment : Tag { }
