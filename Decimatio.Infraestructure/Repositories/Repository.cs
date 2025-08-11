namespace Decimatio.Infraestructure.Repositories
{
    internal sealed class Repository : IRepository
    {
        private readonly DataBaseConfig _connection;

        public Repository(DataBaseConfig connection)
        {
            _connection = connection;    
        }

        public async Task<int> GetTotalCount(string tableName)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = await conn.ExecuteScalarAsync<int>(Querys.GET_TOTAL_COUNT_TABLE, new { Tabla = tableName});
            return result;
        }
    }
}
