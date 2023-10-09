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

        public async Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@IdEvento", idEvento}            
            };
            var dynamicParam = new DynamicParameters(dictionary);
            var result = await _connection.GetListAsync<Sector>("GET_SECTORES_BY_EVENTO", Queries.GET_SECTORES_BY_EVENTO, dynamicParam);
            return result;
        }

        public async Task<Sector> GetById(int idSector)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@IdSector", idSector },
            };

            var dynamicParam = new DynamicParameters( dictionary);
            var result = await _connection.FirstOrDefaultAsync<Sector>("GET_SECTOR_ID", Queries.GET_SECTOR_ID, dynamicParam);
            return result;
        }

        public async Task<int> AddSector(Sector sector)
        {
            var result = await _connection.ExecuteAsync("INSERT_SECTOR", Queries.INSERT_SECTOR, sector);
            return result.Value;
        }
    }
}
