using System.Data;

namespace DSC.Databases;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
