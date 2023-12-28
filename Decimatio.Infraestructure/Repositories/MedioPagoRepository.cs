namespace Decimatio.Infraestructure.Repositories
{
    internal sealed class MedioPagoRepository : IMedioPagoRepository
    {
        private readonly DataBaseConfig _connection;

        public MedioPagoRepository(DataBaseConfig connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<MedioPago>> GetMedioPagos()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<MedioPago>(Querys.GET_MEDIOS_PAGOS);
        }

        public async Task AddMedioPago(MedioPago medioPago)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            await conn.ExecuteScalarAsync<long?>(Querys.INSERT_MEDIO_PAGO, medioPago);
        }

        public async Task<MedioPago> GetMedioPago(int id)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryFirstOrDefaultAsync<MedioPago>(Querys.GET_MEDIO_PAGO, new { IdMedioPago = id });
        }

        public async Task<bool> DeleteMedioPago(int id)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.DELETE_MEDIO_PAGO, new { IdMedioPago = id});
        }

        public async Task<bool> UpdateMedioPago(MedioPago medioPago)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdMedioPago", medioPago.IdMedioPago },
                { "@NombreMedioPago", medioPago.NombreMedioPago },
                { "@Descripcion", medioPago.Descripcion },
                { "@UrlImageBlob", medioPago.UrlImageBlob },
                { "@Activo", medioPago.Activo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.UPDATE_MEDIO_PAGO, dynamicParam);
        }
    }
}
