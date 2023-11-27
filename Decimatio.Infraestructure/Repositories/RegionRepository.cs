namespace Decimatio.Infraestructure.Repositories
{
    internal sealed class RegionRepository : IRegionRepository
    {
        private readonly DataBaseConfig _connection;

        public RegionRepository(DataBaseConfig connection)
        {
            _connection = connection;        
        }

        public async Task<IEnumerable<Domain.Entities.Region>> GetAllRegions()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Domain.Entities.Region>(Querys.GET_REGIONES);
        }
    }
}
