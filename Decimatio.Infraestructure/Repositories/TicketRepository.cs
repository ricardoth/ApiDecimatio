namespace Decimatio.Infraestructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private IDataBaseConnection _connection { get; }

        public TicketRepository(IDataBaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<long> AddTicket(Ticket ticket)
        {
            var result = await _connection.ExecuteScalar("INSERT_TICKET", Queries.INSERT_TICKET, ticket);
            return result.Value;
        }

        public async Task<int> AddTicketQR(TicketQR ticketQR)
        {
            var result = await _connection.ExecuteAsync("INSERT_TICKETQR", Queries.INSERT_TICKETQR, ticketQR);
            return result.Value;
        }

        public async Task<Ticket> GetInfoTicket(long idTicket)
        {
            var result = await _connection.GetListTicketWithObjectAsync<Ticket>("GET_INFO_TICKET", Queries.GET_INFO_TICKET, idTicket);
            if (!result.Any()) throw new Exception("No se encuentra coindidencia para el Ticket");

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Ticket>> GetAllTicket()
        {
            var result = await _connection.GetListTicketWithObjectAsync<Ticket>("GET_TICKETS", Queries.GET_TICKETS, null);
            if (!result.Any()) throw new Exception("No se encuentran tickets ");

            return result;
        }

        public async Task<TicketQR> GetTicketQR(long idTicket)
        {
            var result = await _connection.GetTicketQRAsync<TicketQR>("GET_TICKET_ID", Queries.GET_TICKET_ID, idTicket);
            return result ?? throw new Exception("No se encuentran tickets ");
        }

        public async Task<bool> DeleteDownTicket(long idTicket, bool activo)
        {
            var result = await _connection.ExecuteAsync("DELETE_TICKET", Queries.DELETE_TICKET, new { IdTicket = idTicket, Activo = activo });
            return result > 0;
        
        }
    }
}
