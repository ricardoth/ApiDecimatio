namespace Decimatio.Infraestructure.Repositories
{
    internal sealed class LugarRepository : ILugarRepository
    {
        private readonly DataBaseConfig _connection;

        public LugarRepository(DataBaseConfig connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Lugar>> GetAllLugares()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            var lugares = (await conn.QueryAsync<Lugar, Comuna, Domain.Entities.Region, Lugar>(
                Querys.GET_LUGARES, 
                (lugar, comuna, region) => { 
                    lugar.Comuna = comuna;
                    lugar.Comuna.Region = region;
                    return lugar;
                },
                splitOn: "IdComuna, IdRegion"
            )).ToList();

            return lugares;
        }

        public async Task<Lugar> GetById(int idLugar)
        { 
            using var conn = new SqlConnection(_connection.ConnectionString);
            var lugar = (await conn.QueryAsync<Lugar, Comuna, Domain.Entities.Region, Lugar>(
                Querys.GET_LUGAR_ID,
                (lugar, comuna, region) =>
                {
                    lugar.Comuna = comuna;
                    lugar.Comuna.Region = region;
                    return lugar;
                },
                new { IdLugar = idLugar },
                splitOn: "IdComuna, IdRegion"
            )).FirstOrDefault();

            return lugar;
        }

        public async Task<int> AddLugar(Lugar lugar)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteAsync(Querys.INSERT_LUGAR, lugar);
        }

        public async Task<bool> UpdateLugar(Lugar lugar)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdLugar", lugar.IdLugar },
                { "@IdComuna", lugar.IdComuna },
                { "@NombreLugar", lugar.NombreLugar },
                { "@Ubicacion", lugar.Ubicacion },
                { "@Numeracion", lugar.Numeracion },
                { "@MapaReferencial", lugar.MapaReferencial },
                { "@Activo", lugar.Activo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.UPDATE_LUGAR, dynamicParam);
        }

        public async Task<bool> DeleteLugar(int idLugar)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.DELETE_LUGAR, new { IdLugar = idLugar });
        }
    }
}
