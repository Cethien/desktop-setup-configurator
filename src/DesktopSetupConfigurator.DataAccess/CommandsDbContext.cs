using DesktopSetupConfigurator.Models;
using Microsoft.EntityFrameworkCore;

namespace DesktopSetupConfigurator.Database;
public sealed class CommandsDbContext : DbContext
{
	public CommandsDbContext(DbContextOptions<CommandsDbContext> options)
		: base(options) { }

	public DbSet<InstallationCommand> Commands { get; set; }
	public DbSet<InstallatonStage> InstallationStages { get; set; }
	public DbSet<InstallationProfile> InstallationProfiles { get; set; }
	public DbSet<InstallationEnvironment> InstallationEnvironments { get; set; }
}
