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
            var result = await _connection.GetListAsync<Sector>("GET_ALL_SECTORES", Querys.GET_SECTORES);
            return result;
        }

        public async Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@IdEvento", idEvento}            
            };
            var dynamicParam = new DynamicParameters(dictionary);
            var result = await _connection.GetListAsync<Sector>("GET_SECTORES_BY_EVENTO", Querys.GET_SECTORES_BY_EVENTO, dynamicParam);
            return result;
        }
    }
}
