namespace Decimatio.Infraestructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly DataBaseConfig _connection;

        public EventoRepository(DataBaseConfig connection)
        {
            _connection = connection;        
        }

        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Evento>(Queries.GET_EVENTOS);
        }

        public async Task<Evento> GetById(int idEvento)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = (await conn.QueryAsync<Evento, Lugar, Evento>(
                Queries.GET_EVENTO_ID,
                 (evento, lugar) =>
                 {
                     evento.Lugar = lugar;
                     return evento;
                 },
                new { IdEvento = idEvento },
                splitOn: "NombreLugar"
                )).FirstOrDefault();
            return result;
        }

        public async Task<int> AddEvento(Evento evento)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteAsync(Queries.INSERT_EVENTO, evento);
        }

        public async Task<bool> DeleteEvento(int idEvento)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Queries.DELETE_EVENTO, new { IdEvento = idEvento });
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
                { "@ContenidoFlyer", evento.ContenidoFlyer },
                { "@Activo", evento.Activo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Queries.UPDATE_EVENTO, dynamicParam);
        }
    }
}
