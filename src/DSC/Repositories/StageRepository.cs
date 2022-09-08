using Dapper;

using DSC.Databases;
using DSC.Domain;

namespace DSC.Repositories;

public class StagesRepository : IRepository<Stage>
{
    private readonly IDbConnectionFactory _connFactory;

    public StagesRepository(IDbConnectionFactory connFactory)
    {
        _connFactory = connFactory;
    }
    public async Task<bool> AddAsync(Stage entity)
    {
        using var conn = await _connFactory.CreateConnectionAsync();
        var result = await conn.ExecuteAsync(
            @"INSERT INTO Stages(Name)
            VALUES (@Name)",
            entity
        );
        return result > 0;
    }

    public async Task<IEnumerable<Stage>> GetAllAsync()
    {
        using var connection = await _connFactory.CreateConnectionAsync();
        return
            await connection.QueryAsync<Stage>("SELECT * FROM Stages");
    }

    public Task<Stage> GetByIdAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(object id, Stage updateData)
    {
        throw new NotImplementedException();
    }
}
