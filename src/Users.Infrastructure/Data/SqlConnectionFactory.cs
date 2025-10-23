using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }


    public sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnectionFactory(IOptions<DbOptions> options)
        {
            _connectionString = options.Value.ConnectionString ?? string.Empty;
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
