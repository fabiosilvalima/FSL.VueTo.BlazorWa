using FSL.Framework.Core.Configuration.Models;
using FSL.Framework.Core.Extensions;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FSL.Framework.Core.Repository
{
    public class SqlRepository
    {
        private string _connectionStringId;
        private string _connectionString;
        private readonly IDefaultConfiguration _configuration;

        public SqlRepository(
            IDefaultConfiguration configuration)
        {
            _configuration = configuration;
            UseConnectionStringId(configuration?.ConnectionStringId ?? "Default");
        }

        protected async Task<T> WithConnectionAsync<T>(
            Func<SqlConnection, Task<T>> getData,
            string connectionString = null)
        {
            using (var connection = CreateConnection(connectionString))
            {
                await connection.OpenAsync();

                var data = await getData(connection);

                connection.Close();

                return data;
            };
        }

        protected SqlRepository UseConnectionStringId(
            string connectionStringId)
        {
            _connectionStringId = connectionStringId;

            return this;
        }

        protected SqlRepository UseConnectionString(
            string connectionString)
        {
            _connectionString = connectionString;

            return this;
        }

        private SqlConnection CreateConnection(
            string connectionString = null)
        {
            if (!connectionString.IsNullOrEmpty())
            {
                return new SqlConnection(connectionString);
            }

            if (_connectionString.IsNullOrEmpty())
            {
                _connectionString = _configuration.GetConnectionString(_connectionStringId);
            }

            return new SqlConnection(_connectionString);
        }
    }
}
