namespace Decimatio.Infraestructure.Repositories
{
    public class AccesoEventoRepository : IAccesoEventoRepository
    {
        private readonly IDataBaseConnection _connection;

        public AccesoEventoRepository(IDataBaseConnection connection)
        {
            _connection = connection;        
        }

        public async Task<int> RegistroAccesoEvento(AccesoEvento accesoEvento)
        {
            var result = await _connection.ExecuteAsync("INSERT_ACCESO_EVENTO_IN", Queries.INSERT_ACCESO_EVENTO_IN, accesoEvento);
            return result.Value;
        }

        public async Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdTicket", ticketAcceso.IdTicket },
                { "@Rut", ticketAcceso.Rut },
                { "@Dv", ticketAcceso.Dv },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            var result = await _connection.FirstOrDefaultAsync<AccesoEventoStatus>("VALIDAR_ACCESO_TICKET", Queries.VALIDAR_ACCESO_TICKET, dynamicParam);
            return result;
        }
    }
}
