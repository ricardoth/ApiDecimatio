using Decimatio.Domain.Entities;
using System;

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

        public async Task<int> AddEvento(Evento evento)
        {
            var result = await _connection.ExecuteAsync("INSERT_EVENTO", Queries.INSERT_EVENTO, evento);
            return result.Value;
        }

        public async Task<bool> DeleteEvento(int idEvento)
        {
            var result = await _connection.ExecuteScalar<bool>("DELETE_EVENTO", Queries.DELETE_EVENTO, new { IdEvento = idEvento });
            return result;
        }

        public async Task<bool> UpdateEvento(Evento evento)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdEvento", evento.IdEvento },
                { "@IdLugar", evento.IdLugar },
                { "@NombreEvento", evento.NombreEvento },
                { "@Direccion", evento.Direccion },
                { "@Fecha", evento.Fecha },
                { "@Flyer", evento.Flyer },
                { "@Activo", evento.Activo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            var result = await _connection.ExecuteScalar<bool>("UPDATE_EVENTO", Queries.UPDATE_EVENTO, dynamicParam);
            return result;
        }
    }
}
