using DSC.Domain;

namespace DSC.Services;

public interface ICommandService
{
    IEnumerable<ShellCommand> ListInstallCommands();
    ShellCommand AddInstallCommand(ShellCommand cmd);
    ShellCommand RemoveInstallCommand(ShellCommand cmd);
    IEnumerable<ShellCommand> ListPostInstallCommands();
    ShellCommand AddPostInstallCommand(ShellCommand cmd);
    ShellCommand RemovePostInstallCommand(ShellCommand cmd);
}

public interface IProfileService
{
    IEnumerable<InstallProfile> ListProfiles();
    InstallProfile AddProfile(InstallProfile profile);
    InstallProfile RemoveProfile(InstallProfile profile);
    InstallProfile RenameProfile(InstallProfile profile, string newName);
}