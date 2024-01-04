using System.Data;
using Npgsql;
using UniIdentity.Application.Contracts.Data;

namespace UniIdentity.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var con = new NpgsqlConnection(_connectionString);
        con.Open();

        return con;
    }
}