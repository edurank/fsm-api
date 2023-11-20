using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace fsmAPI.Services.Repositories
{
    public class DBManager
    {
        private string ENVIRONMENT = "DEV";
        private string connectionURL = string.Empty;

        // string connection... 

        public DBManager(string _ENVIRONMENT = null)
        {
            ENVIRONMENT = _ENVIRONMENT;
            connectionURL = "Data Source=localhost;Initial Catalog=fsm;User ID =sa;Password=;TrustServerCertificate=True";
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, object? queryParams = null)
        {
            using (IDbConnection conn = new SqlConnection(connectionURL))
            {
                var response = await conn.QueryAsync<T>(query, queryParams, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string query, object? queryParams = null)
        {
            using (IDbConnection conn = new SqlConnection(connectionURL))
            {
                return await conn.ExecuteScalarAsync<T>(query, queryParams, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> ExecuteQuerySingle<T>(string query)
        {
            using (IDbConnection conn = new SqlConnection(connectionURL))
            {
                var response = await conn.QuerySingleAsync<T>(query);
                return response;
            }
        }
        
        public async Task<IEnumerable<T>> ExecuteQuery<T>(string query, object? queryParams = null)
        {
            using (IDbConnection conn = new SqlConnection(connectionURL))
            {
                var response = await conn.QueryAsync<T>(query);
                return response;
            }
        }

        public async Task<T> ExecuteScalarQuery<T>(string query, object? queryParams = null)
        {
            using (IDbConnection conn = new SqlConnection(connectionURL))
            {
                return await conn.ExecuteScalarAsync<T>(query, queryParams, commandType: CommandType.Text);
            }
        }

        public async Task<int> SelectUsers(string query)
        {
            using (IDbConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();
                dynamic result = await connection.QueryAsync<dynamic>(query);
                return 1;
            }
        }
    }
}

