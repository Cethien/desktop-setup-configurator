using System.Data;

using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace DSC.Databases;

public class SqliteConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("SqliteConnection");
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var conn = new SqliteConnection(_connectionString);
        await conn.OpenAsync();
        return conn;
    }
}