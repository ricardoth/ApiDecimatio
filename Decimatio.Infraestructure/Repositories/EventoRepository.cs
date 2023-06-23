namespace Decimatio.Infraestructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly IDataBaseConnection _connection;

        public EventoRepository(IDataBaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
            var result = await _connection.GetListAsync<Evento>("GET_EVENTOS", Querys.GET_EVENTOS);
            return result;
        }
    }
}
