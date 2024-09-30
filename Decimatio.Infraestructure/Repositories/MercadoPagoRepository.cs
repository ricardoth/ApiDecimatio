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
    }
}
