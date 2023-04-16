using Dapper;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Decimatio.Infraestructure.Connection
{
    public class DataBaseConnection : IDataBaseConnection
    {
        private readonly DataBaseConfig _connection;
        private readonly Guid _key;
        public DataBaseConnection(DataBaseConfig connection) 
        { 
            _connection = connection;
            _key = Guid.NewGuid();
        }
        
        public async Task<int?> ExecuteAsync(string queryName, string query, object entity)
        {
            DateTime startTime = DateTime.Now;
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool isSuccess = true;

            try
            {
                using (var conn = new SqlConnection(_connection.ConnectionString))
                {
                    return await conn.ExecuteAsync(query, entity);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            finally { stopwatch.Stop(); }
        }

        public async Task<long?> ExecuteScalar(string queryName, string query, object entity)
        {
            DateTime startTime = DateTime.Now;
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool isSuccess = true;

            try
            {
                using (var conn = new SqlConnection(_connection.ConnectionString))
                {
                    long newId = await conn.ExecuteScalarAsync<long>(query, entity);
                    return newId;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            finally { stopwatch.Stop(); }
        }

        public async Task<T> FirstOrDefaultAsync<T>(string queryName, string query, object entity)
        {
            var st = DateTime.Now;
            var w = Stopwatch.StartNew();
            var success = true;

            try
            {
                using var conn = new SqlConnection(_connection.ConnectionString);
                return await conn.QueryFirstOrDefaultAsync<T>(query);
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            finally
            {
                w.Stop();
            }
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string queryName, string query)
        {
            var st = DateTime.Now;
            var w = Stopwatch.StartNew();
            var success = true;

            try
            {
                using var conn = new SqlConnection(_connection.ConnectionString);
                return await conn.QueryAsync<T>(query);
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            finally
            {
                w.Stop();
            }

        }
    }
}
