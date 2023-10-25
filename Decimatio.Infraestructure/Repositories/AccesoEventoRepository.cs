namespace Decimatio.Infraestructure.Repositories
{
    public class AccesoEventoRepository : IAccesoEventoRepository
    {
        private readonly DataBaseConfig _connection;

        public AccesoEventoRepository(DataBaseConfig connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<AccesoEventoTicket>> GetAllAccesoEventoTickets()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<AccesoEventoTicket>(Queries.GET_ACCESOS_TICKET);
        }

        public async Task<int> RegistroAccesoEvento(AccesoEvento accesoEvento)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteAsync(Queries.INSERT_ACCESO_EVENTO_IN, accesoEvento);
        }

        public async Task<int> SalidaAccesoEvento(long idAccesoEvento)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteAsync(Queries.INSERT_ACCESO_EVENTO_OUT, new { IdAccesoEvento = idAccesoEvento });
        }

        public async Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdTicket", ticketAcceso.IdTicket },
                { "@Rut", ticketAcceso.Rut },
                { "@Dv", ticketAcceso.Dv },
                { "@IdEvento", ticketAcceso.IdEvento },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryFirstOrDefaultAsync<AccesoEventoStatus>(Queries.VALIDAR_ACCESO_TICKET, dynamicParam);
        }
    }
}
