using DesktopSetupConfigurator.Database.Models.Common;

namespace DesktopSetupConfigurator.Models;
public sealed class InstallationCommand : DbModel
{
	public required string Text { get; set; } = default!;
	public required InstallatonStage Stage { get; set; } = default!;
	public required InstallationProfile[] Profiles { get; set; } = default!;
	public required InstallationEnvironment[] Environments { get; set; } = default!;
}
