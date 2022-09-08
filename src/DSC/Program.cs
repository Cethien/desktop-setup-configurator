using DSC;
using DSC.Databases;
using DSC.Domain;
using DSC.Repositories;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//build
var configBuilder = new ConfigurationBuilder();
configBuilder.BuildConfig();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IDbConnectionFactory, SqliteConnectionFactory>();
        services.AddSingleton<DbInitializer>();
        services.AddSingleton<IRepository<InstallProfile>, InstallProfileRepository>();
        services.AddSingleton<IRepository<ShellCommand>, CommandsRepository>();
        services.AddSingleton<IRepository<Stage>, StagesRepository>();
    })
    .Build();

//run program
var dbInitializer = host.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

var stagesRepo = host.Services.GetRequiredService<IRepository<Stage>>();
var profilesRepo = host.Services.GetRequiredService<IRepository<InstallProfile>>();
var cmdsRepo = host.Services.GetRequiredService<IRepository<ShellCommand>>();

// get data
var stages = await stagesRepo.GetAllAsync();
var profiles = await profilesRepo.GetAllAsync();
var cmds = await cmdsRepo.GetAllAsync();

//create some data
// new Stage[] {
//     new(){Name = "install"},
//     new(){Name = "post"}
// }.ToList()
// .ForEach(async x => await stagesRepo.AddAsync(x));

// new InstallProfile[] {
//     new(){Name = "any", IsDefault = true},
//     new(){Name = "work"},
//     new(){Name = "home"}
// }.ToList()
// .ForEach(async x => await profilesRepo.AddAsync(x));

// new ShellCommand[] {
//     new() {
//         Command = "winget install --id -e Microsoft.PowerShell --source winget",
//         Stage = stages.FirstOrDefault(x => x.Name == "install"),
//         InstallProfile = profiles.FirstOrDefault(x => x.Name == "any")
//         },
//     new() {
//         Command = "winget install --id -e Git.Git --source winget",
//         Stage = stages.FirstOrDefault(x => x.Name == "install"),
//         InstallProfile = profiles.FirstOrDefault(x => x.Name == "any")
//         },
//     new() {
//         Command = "winget install --id -e Github.Cli --source winget",
//         Stage = stages.FirstOrDefault(x => x.Name == "install"),
//         InstallProfile = profiles.FirstOrDefault(x => x.Name == "any")
//         }
// }.ToList()
// .ForEach(async x => await cmdsRepo.AddAsync(x));

// print data
System.Console.WriteLine("Stages:");
stages.ToList()
    .ForEach(x => System.Console.WriteLine("\t{0}",
                                           x.Name));

System.Console.WriteLine("Profiles:");
profiles.ToList()
    .ForEach(x => System.Console.WriteLine("\t{0}\t(default: {1})",
                                           x.Name,
                                           x.IsDefault));

System.Console.WriteLine("Commands:");
cmds.ToList()
    .ForEach(x => System.Console.WriteLine("\t{0}\n\t(profile: {1}; stage: {2})",
                                           x.Command,
                                           x.InstallProfile?.Name,
                                           x.Stage?.Name));

//print json
var config = new
{
    profiles = profiles.Select(x => x.Name),
    defaultProfile = profiles.Where(x => x.IsDefault).Select(x => x.Name).FirstOrDefault(),
    install = cmds.Where(x => x.Stage.Name == "install").Select(x => new
    {
        command = x.Command,
        profile = x.InstallProfile.Name
    }),
    post = cmds.Where(x => x.Stage.Name == "post").Select(x => new
    {
        command = x.Command,
        profile = x.InstallProfile.Name
    })
};

System.Console.WriteLine(config.AsJson());