namespace Decimatio.Infraestructure.Repositories
{
    public class MercadoPagoRepository : IMercadoPagoRepository
    {
        private readonly DataBaseConfig _connection;

        public MercadoPagoRepository(DataBaseConfig connection)
        {
            _connection = connection;   
        }


        public async Task<IEnumerable<PreferenceTicket>> GetAll()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = await conn.QueryAsync<PreferenceTicket,MerchantOrder, Usuario, Evento, Sector, PreferenceTicket>(
                Querys.GET_PREFERENCE_TICKET_MERCADO_PAGO,
                (preference, merchantOrder, usuario, evento, sector) =>
                {
                    preference.MerchantOrder = merchantOrder;
                    preference.Usuario = usuario;
                    preference.Evento = evento;
                    preference.Sector = sector;
                    return preference;
                },
                splitOn: "IdMerchantOrder,IdUsuario,IdEvento,IdSector");
            return result;
        }

        public async Task<IEnumerable<PreferenceTicket>> GetByPreferenceCode(string preferenceCode)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);
            var result = await connection.QueryAsync<PreferenceTicket>(Querys.GET_PREFERENCE_TICKET_BY_PREFERENCE_CODE, new { PreferenceCode = preferenceCode});
            return result;
        }
    }
}
