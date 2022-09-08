using Dapper;

using DSC.Databases;
using DSC.Domain;

namespace DSC.Repositories;

public class CommandsRepository : IRepository<ShellCommand>
{
    private readonly IDbConnectionFactory _connFactory;

    public CommandsRepository(IDbConnectionFactory connFactory)
    {
        _connFactory = connFactory;
    }

    public async Task<bool> AddAsync(ShellCommand entity)
    {
        using var conn = await _connFactory.CreateConnectionAsync();
        var result = await conn.ExecuteAsync(
            @"INSERT INTO ShellCommands(Command, StageId, InstallProfileId)
            VALUES (@Command, @StageId, @InstallProfileId)",
            new
            {
                Command = entity.Command,
                StageId = entity.Stage?.StageId,
                InstallProfileId = entity.InstallProfile?.InstallProfileId
            }
        );
        return result > 0;
    }

    public async Task<IEnumerable<ShellCommand>> GetAllAsync()
    {
        using var conn = await _connFactory.CreateConnectionAsync();

        var sql = @"SELECT c.*, s.*, p.* 
        FROM ShellCommands c 
        INNER JOIN Stages s ON c.StageId = s.StageId
        INNER JOIN InstallProfiles p ON c.InstallProfileId = p.InstallProfileId";

        return
            await conn.QueryAsync<ShellCommand, Stage, InstallProfile, ShellCommand>(sql, (cmd, stage, profile) =>
            {
                cmd.Stage = stage;
                cmd.InstallProfile = profile;
                return cmd;
            },
            splitOn: "InstallProfileId, StageId, Name, InstallProfileId");
    }

    public Task<ShellCommand> GetByIdAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(object id, ShellCommand updateData)
    {
        throw new NotImplementedException();
    }
}
