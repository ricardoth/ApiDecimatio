using Decimatio.Domain.Entities;

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
            var result = await _connection.GetListAsync<Evento>("GET_EVENTOS", Queries.GET_EVENTOS);
            return result;
        }

        public async Task<Evento> GetById(int idEvento)
        {
            var result = await _connection.FirstOrDefaultEventoWithObjectAsync<Evento>("GET_EVENTO_ID", Queries.GET_EVENTO_ID, idEvento);
            return result;
        }
    }
}
