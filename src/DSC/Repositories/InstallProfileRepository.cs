using DSC.Databases;
using DSC.Domain;
using Dapper;

namespace DSC.Repositories;

public class InstallProfileRepository : IRepository<InstallProfile>
{
    private readonly IDbConnectionFactory _connFactory;

    public InstallProfileRepository(IDbConnectionFactory connFactory)
    {
        _connFactory = connFactory;
    }

    public async Task<bool> AddAsync(InstallProfile entity)
    {
        using var conn = await _connFactory.CreateConnectionAsync();
        var result = await conn.ExecuteAsync(
            @"INSERT INTO InstallProfiles(Name, IsDefault)
            VALUES (@Name, @IsDefault)",
            entity
        );
        return result > 0;
    }

    public async Task<IEnumerable<InstallProfile>> GetAllAsync()
    {
        using var connection = await _connFactory.CreateConnectionAsync();
        return
            await connection.QueryAsync<InstallProfile>("SELECT * FROM InstallProfiles");
    }

    public Task<InstallProfile> GetByIdAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(object id, InstallProfile updateData)
    {
        throw new NotImplementedException();
    }
}
