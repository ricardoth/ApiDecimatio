namespace Decimatio.Infraestructure.Repositories
{
    public class MedioPagoRepository : IMedioPagoRepository
    {
        private IDataBaseConnection _connection;

        public MedioPagoRepository(IDataBaseConnection connection)
        {
            _connection = connection;
        }
        

        public async Task<IEnumerable<MedioPago>> GetMedioPagos()
        {
            var result = await _connection.GetListAsync<MedioPago>("GET_MEDIOS_PAGOS", Querys.GET_MEDIOS_PAGOS);
            return result;
        }

        public async Task AddMedioPago(MedioPago medioPago)
        {
            await _connection.ExecuteScalar("INSERT_MEDIO_PAGO", Querys.INSERT_MEDIO_PAGO, medioPago);
        }

        public async Task<MedioPago> GetMedioPago(int id)
        {
            var result = await _connection.FirstOrDefaultAsync<MedioPago>("GET_MEDIO_PAGO", Querys.GET_MEDIO_PAGO,  new { IdMedioPago = id });
            return result;
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
