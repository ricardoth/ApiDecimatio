namespace Decimatio.Infraestructure.Repositories
{
    internal sealed class EventoRepository : IEventoRepository
    {
        private readonly DataBaseConfig _connection;

        public EventoRepository(DataBaseConfig connection)
        {
            _connection = connection;        
        }

        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return (await conn.QueryAsync<Evento, Lugar, Evento>(Querys.GET_EVENTOS,
                (evento, lugar) => { 
                    evento.Lugar = lugar;
                    return evento;
                },
                splitOn: "IdLugar"
                )).ToList();
        }

        public async Task<Evento> GetById(int idEvento)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = (await conn.QueryAsync<Evento, Lugar, Evento>(
                Querys.GET_EVENTO_ID,
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
            return await conn.ExecuteAsync(Querys.INSERT_EVENTO, evento);
        }

        public async Task<bool> DeleteEvento(int idEvento)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.DELETE_EVENTO, new { IdEvento = idEvento });
        }

        public async Task<bool> UpdateEvento(Evento evento)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdEvento", evento.IdEvento },
                { "@IdLugar", evento.IdLugar },
                { "@NombreEvento", evento.NombreEvento },
                { "@Descripcion", evento.Descripcion },
                { "@Fecha", evento.Fecha },
                { "@Flyer", evento.Flyer },
                { "@Observacion", evento.Observacion },
                { "@ProductoraResponsable", evento.ProductoraResponsable },
                { "@Banner", evento.Banner },
                { "@ContenidoBanner", evento.ContenidoBanner },
                { "@Activo", evento.Activo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.UPDATE_EVENTO, dynamicParam);
        }

        public async Task<IEnumerable<Evento>> GetEventosFilter(string filtro)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@Filtro", filtro }
            };

            var dynamicParam = new DynamicParameters(dictionary);

            using var conn = new SqlConnection(_connection.ConnectionString);

            var result = (await conn.QueryAsync<Evento, Lugar, Evento>(
                Querys.GET_EVENTOS_FILTRO,
                (evento, lugar) =>
                {
                    evento.Lugar = lugar;
                    return evento;
                },
                dynamicParam,
                splitOn: "IdLugar"
                )).Distinct().ToList();

            return result;
        }
    }
}
