using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PeDeMeia.Infra.Data
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}