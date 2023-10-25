namespace Decimatio.Infraestructure.Repositories
{
    public class LugarRepository : ILugarRepository
    {
        private readonly DataBaseConfig _connection;

        public LugarRepository(DataBaseConfig connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Lugar>> GetAllLugares()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Lugar>(Queries.GET_LUGARES);
        }
    }
}
