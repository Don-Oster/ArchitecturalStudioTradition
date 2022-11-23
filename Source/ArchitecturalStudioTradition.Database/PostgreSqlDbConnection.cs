using ArchitecturalStudioTradition.Database.Configuration;
using Npgsql;
using System.Data.Common;

namespace ArchitecturalStudioTradition.Database
{
    internal class PostgreSqlDbConfiguration
    {
        private readonly NpgsqlConnectionStringBuilder _builder;

        public PostgreSqlDbConfiguration(IPostgreSqlConfig config)
        {
            _builder = new NpgsqlConnectionStringBuilder
            {
                Host = config.DbHost,
                Port = config.DbPort,
                Database = config.DbName,
                Username = config.DbUsername,
                Password = config.DbPassword
            };
        }

        public DbConnection Connection(bool open = true)
        {
            var connection = new NpgsqlConnection(_builder.ToString());
            if (open) connection.Open();
            return connection;
        }
    }
}
