namespace Decimatio.Infraestructure.Repositories
{
    internal sealed class ComunaRepository : IComunaRepository
    {
        private readonly DataBaseConfig _connection;

        public ComunaRepository(DataBaseConfig connection)
        {
            _connection = connection;                
        }

        public async Task<IEnumerable<Comuna>> GetComunasByRegion(int idRegion)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Comuna>(Querys.GET_COMUNAS_BY_REGION, new { IdRegion = idRegion });
        }
    }
}
