namespace Decimatio.Infraestructure.Repositories
{
    public class LugarRepository : ILugarRepository
    {
        private readonly IDataBaseConnection _connection;

        public LugarRepository(IDataBaseConnection connection)
        {
            _connection = connection;                
        }

        public async Task<IEnumerable<Lugar>> GetAllLugares()
        {
            return await _connection.GetListAsync<Lugar>("GET_LUGARES", Queries.GET_LUGARES);
        }
    }
}
