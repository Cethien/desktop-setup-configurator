using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using DesktopSetupConfigurator.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesktopSetupConfigurator;
class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("an autistic app");

        // init
        var setupCommand = new Command("setup", "Setup");
        setupCommand.SetHandler<IHost>(SetupHandler, null);
        rootCommand.AddCommand(setupCommand);

        // commands
        var commandsCommand = new Command("commands", "Manage Commands");
        commandsCommand.SetHandler(() =>
        {
            System.Console.WriteLine("commands Called");
        });
        rootCommand.AddCommand(commandsCommand);

        // profiles
        var profilesCommand = new Command("profiles", "Manage Profiles");
        profilesCommand.SetHandler(() =>
        {
            System.Console.WriteLine("profiles Called");
        });
        rootCommand.AddCommand(profilesCommand);

        // stages
        var stagesCommand = new Command("stages", "Manage Stages");
        stagesCommand.SetHandler(() =>
        {
            System.Console.WriteLine("stages Called");
        });
        rootCommand.AddCommand(stagesCommand);

        // environments
        var environmentsCommand = new Command("environments", "Manage environments");
        environmentsCommand.SetHandler(() =>
        {
            System.Console.WriteLine("environments Called");
        });
        rootCommand.AddCommand(environmentsCommand);

        var cliBuilder = new CommandLineBuilder(rootCommand)
            .UseHost(builder => builder.ConfigureHost())
            .UseDefaults();

        var parser = cliBuilder.Build();
        return await parser.InvokeAsync(args).ConfigureAwait(false);
    }

    private static void SetupHandler(IHost host)
    {
        var svc = host.Services.GetRequiredService<IDataService>();
        svc.Setup();
        System.Console.WriteLine("DB was Set Up!");
    }
}
