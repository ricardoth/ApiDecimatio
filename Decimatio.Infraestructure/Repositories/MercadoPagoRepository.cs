using Decimatio.Domain.MercadoPagoEntitites;

namespace Decimatio.Infraestructure.Repositories
{
    public class MercadoPagoRepository : IMercadoPagoRepository
    {
        private readonly DataBaseConfig _connection;

        public MercadoPagoRepository(DataBaseConfig connection)
        {
            _connection = connection;   
        }

        public async Task<int> AddNotificationPayment(MercadoPagoNotification notification)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@PaymentId", notification.Data.Id },
                { "@LiveMode", notification.LiveMode }
              
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = await conn.ExecuteAsync(Querys.INSERT_NOTIFICATION_MERCADOPAGO, dynamicParam);
            return result;
        }

        public async Task<IEnumerable<PreferenceTicket>> GetAll()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = await conn.QueryAsync<PreferenceTicket, Usuario, Evento, Sector, PreferenceTicket>(
                Querys.GET_PREFERENCE_TICKET_MERCADO_PAGO,
                (preference, usuario, evento, sector) =>
                {
                    preference.Usuario = usuario;
                    preference.Evento = evento;
                    preference.Sector = sector;
                    return preference;
                },
                splitOn: "IdUsuario,IdEvento,IdSector");
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
