namespace Decimatio.Infraestructure.Repositories
{
    public class MedioPagoRepository : IMedioPagoRepository
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

        public async Task<int> DeleteMedioPago(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMedioPago(int id, MedioPago medioPago)
        {
            throw new NotImplementedException();
        }
    }
}
