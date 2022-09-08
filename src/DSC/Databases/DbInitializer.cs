using Dapper;

namespace DSC.Databases;

public class DbInitializer
{
    private readonly IDbConnectionFactory _connFactory;

    public DbInitializer(IDbConnectionFactory connFactory)
    {
        _connFactory = connFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connFactory.CreateConnectionAsync();

        new string[]{
            @"CREATE TABLE IF NOT EXISTS Stages (
                StageId INTEGER PRIMARY KEY AUTOINCREMENT, 
                Name TEXT
            )",
            @"CREATE TABLE IF NOT EXISTS InstallProfiles (
                InstallProfileId INTEGER PRIMARY KEY AUTOINCREMENT, 
                Name TEXT,
                IsDefault INTEGER
            )",
            @"CREATE TABLE IF NOT EXISTS ShellCommands (
                CommandId INTEGER PRIMARY KEY AUTOINCREMENT, 
                Command TEXT,
                StageId INTEGER,
                InstallProfileId INTEGER,
                FOREIGN KEY(StageId) REFERENCES Stages(StageId)
                FOREIGN KEY(InstallProfileId) REFERENCES InstallProfiles(InstallProfileId)
            )"
        }.ToList()
        .ForEach(async x => await connection.ExecuteAsync(x));
    }
}
