using System.Data;

namespace UniIdentity.Application.Contracts.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}