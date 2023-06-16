namespace Decimatio.Infraestructure.Repositories
{
    public class SectorRepository : ISectorRepository
    {
        private readonly IDataBaseConnection _connection;

        public SectorRepository(IDataBaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Sector>> GetAllSectores()
        {
            var result = await _connection.GetListAsync<Sector>("GET_ALL_SECTORES", Queries.GET_SECTORES);
            return result;
        }
    }
}
